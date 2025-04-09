using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.Steam
{
    public class SteamResourceHolder : MonoBehaviour
    {
        public delegate void SteamResourceDelegate(float amount,bool visuals, SteamResourceHolder holder);

        public SteamResourceDelegate OnInstantTransferToBegin;
        public SteamResourceDelegate OnInstantTransferFromBegin;
        
        [SerializeField]
        private UnityEvent OnSteamFull;
        [SerializeField]
        private UnityEvent OnSteamEmpty;
        private class SteamTransferContext
        {
            private SteamResourceHolder _giver;
            private SteamResourceHolder _receiver;

            private float _totalAmount;
            private float _amountLeft;
            private float _totalTime;
            private float _timeLeft;
            
            public SteamTransferContext(SteamResourceHolder giver, SteamResourceHolder receiver, float amount, float time)
            {
                _giver = giver;
                _receiver = receiver;
                _totalAmount = amount;
                _amountLeft = amount;
                _totalTime = time;
                _timeLeft = time;
            }

            public void ExecuteTransferFrame(float deltaTime)
            {
                float amountToTransfer = _totalAmount * (deltaTime / _totalTime);
                
                if(amountToTransfer > _giver.SteamAmount)
                {
                    //if giver runs out, mark this transfer for early completion
                    _amountLeft = 0;
                    _timeLeft = 0;
                }
                
                _giver.RemoveSteam(amountToTransfer);
                _receiver.AddSteam(amountToTransfer);
                _amountLeft -= amountToTransfer;
                _timeLeft -= deltaTime;
            }
            
            public bool IsComplete()
            {
                return _amountLeft <= 0 || _timeLeft <= 0;
            }
            
        }

        //if less than 0, defaults to max
        [field: SerializeField] public float SteamAmount { get; private set; } = -1;
        [field: SerializeField] public float MaxSteamAmount { get; private set; } = 10;
        [field: SerializeField] public float SteamRegenRate { get; private set; } = 0;
        
        public float SteamFillPercent{get => SteamAmount / MaxSteamAmount;}
        private List<SteamTransferContext> _activeTransfers = new List<SteamTransferContext>();
        [SerializeField] private float _postTransferRegenCooldown = 1;
        private float _regenCooldownTimer = 0;

        private void Awake()
        {
            if (SteamAmount < 0)
            {
                SteamAmount = MaxSteamAmount;
            }
        }

        public void BeginSteamTransferTo(SteamResourceHolder receiver, float amount, float time = 0, bool visuals = false)
        {
            _regenCooldownTimer = _postTransferRegenCooldown;

            if (time == 0)
            {
                float giverAmount = SteamAmount;

                if (giverAmount < amount)
                {
                    amount = giverAmount;
                }
                
                float amountAdded = receiver.AddSteam(amount);
                RemoveSteam(amountAdded);
                
                
                OnInstantTransferToBegin?.Invoke(amountAdded, visuals, receiver);
                receiver.OnInstantTransferFromBegin?.Invoke(amountAdded, visuals, this);
                return;
            }

            SteamTransferContext newTransfer = new SteamTransferContext(this, receiver, amount, time);
            AddTransfer(newTransfer);
        }

        public void BeginSteamTransferFrom(SteamResourceHolder giver, float amount, float time = 0, bool visuals = false)
        {
            giver.BeginSteamTransferTo(this, amount, time, visuals);
        }

        private float RemoveSteam(float amount)
        {
            float oldSteam = SteamAmount;

            SteamAmount -= amount;
            
            float amountTransferred = amount;

            if (SteamAmount < 0)
            {
                amountTransferred += SteamAmount;

                SteamAmount = 0;
            }
            
            if (Mathf.Approximately(SteamAmount, 0) && oldSteam > 0)
            {
                OnSteamEmpty?.Invoke();
            }

            return amountTransferred;
        }

        private float AddSteam(float amount)
        {
            float oldSteam = SteamAmount;
            SteamAmount += amount;


            float amountTransferred = amount;
            if (SteamAmount > MaxSteamAmount)
            {
                amountTransferred -= SteamAmount - MaxSteamAmount;
                
                SteamAmount = MaxSteamAmount;
            }

            if (Mathf.Approximately(SteamAmount, MaxSteamAmount) && oldSteam < MaxSteamAmount)
            {
                OnSteamFull?.Invoke();
            }

            return amountTransferred;
        }
        
        private void Update()
        {

            HandleAllTransfers();

            if (_activeTransfers.Count > 0)
            {
                _regenCooldownTimer = _postTransferRegenCooldown;
            }

            if (_regenCooldownTimer > 0)
            {
                _regenCooldownTimer -= Time.deltaTime;
            }
            
            HandleRegeneration();
        }

        private void HandleRegeneration()
        {
            if(_regenCooldownTimer > 0)
            {
                return;
            }
            
            if (SteamRegenRate > 0)
            {
                AddSteam(SteamRegenRate * Time.deltaTime);
            }
            else if (SteamRegenRate < 0)
            {
                RemoveSteam(-SteamRegenRate * Time.deltaTime);
            }
        }

        private void HandleAllTransfers()
        {
            foreach (var transfer in _activeTransfers.ToList())
            {
                transfer.ExecuteTransferFrame(Time.deltaTime);
                if (transfer.IsComplete())
                {
                    _activeTransfers.Remove(transfer);
                }
            }
        }

        private void AddTransfer(SteamTransferContext context)
        {
            _activeTransfers.Add(context);
        }

        public void SetSteamAmount(float amount)
        {
            SteamAmount = amount;
        }
    }
}