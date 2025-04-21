using System;
using UnityEngine;

namespace Systems.CombatTokens
{
    public class CombatTokenManager : MonoBehaviour
    {
        public class Token
        {
            private GameObject _tokenUserInternal;
            
            public bool IsTokenUsed()
            {
                return _tokenUserInternal;
            }
            
            public void AllocateToken(GameObject tokenUser)
            {
                if (tokenUser == null) return;
                
                _tokenUserInternal = tokenUser;
            }
            
            public void DeallocateToken()
            {
                _tokenUserInternal = null;
            }
            
        }
        
        
        public static CombatTokenManager Instance;

        [SerializeField]
        private int _tokenCount = 3;

        private Token[] _tokens;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _tokens = new Token[_tokenCount];

            for (int i = 0; i < _tokenCount; i++)
            {
                _tokens[i] = new Token();
            }
        }


        //Call request token to get a free token. Call Deallocate on the token to return it to the pool.
        public Token RequestToken(GameObject tokenUser)
        {
            for (int i = 0; i < _tokenCount; i++)
            {
                if (!_tokens[i].IsTokenUsed())
                {
                    _tokens[i].AllocateToken(tokenUser);
                    return _tokens[i];
                }
            }

            return null;
        }
    }
}