using UnityEngine.SceneManagement;

public static class PlayerStatUtil
{
    private static PlayerController[] _playerControllers = new PlayerController[2];
    private static Attack[] _playerAttacks = new Attack[2];
    private static Damageable[] _playerDamageables = new Damageable[2];
    
    public static readonly string[] SceneExcludeFromGettingComponents = new string[]{"StartMenu", "Background Story",};
    public static readonly string[] SceneExcludeFromStatRestore = new string[]{"StartMenu", "Background Story", "GameplayScene", 
        "Level1"};
    public static readonly string[] SceneExcludeFromStatRecord = new string[]{"StartMenu", "Background Story", "GameplayScene"};
    private struct CatStats
    {
        public static int CurrentHealth;
        public static int MaxHealth;

        public static int CurrentMp;
        public static int MaxMp;

        public static int AttackDamage;
    }

    private struct DogStats
    {
        public static int CurrentHealth;
        public static int MaxHealth;

        public static int CurrentMp;
        public static int MaxMp;

        public static int AttackDamage;
    }
    
        public static void IncreasePlayerHp(int increaseAmount)
        {
             foreach (var d in _playerDamageables)
             {
                 d.MaxHealth += increaseAmount;
             }
        }

        public static void IncreasePlayerMp(int increaseAmount)
        {
            foreach (var d in _playerDamageables)
            {
                d.MaxMP += increaseAmount;
            }
        }

        public static void IncreaseAttackDamage(int increaseAmount)
        {
            foreach (var d in _playerAttacks)
            {
                d.attackDamage += increaseAmount;
            }
        }
        
        // 获取最新的操纵对象! 否则有可能找不到对应的对象.
        public static void GetComponents()
        {
            _playerControllers[0] = GameManager.Instance.cat.GetComponent<PlayerController>();
            _playerControllers[1] = GameManager.Instance.dog.GetComponent<PlayerController>();
            _playerAttacks[0] = GameManager.Instance.cat.GetComponentInChildren<Attack>();
            _playerAttacks[1] = GameManager.Instance.dog.GetComponentInChildren<Attack>();
            _playerDamageables[0] = GameManager.Instance.cat.GetComponent<Damageable>();
            _playerDamageables[1] = GameManager.Instance.dog.GetComponent<Damageable>();
        }

        public static void RecordPlayerStats()
        {
             // 注意，猫的下标是0，狗的下标是1
             CatStats.CurrentHealth = _playerDamageables[0].Health;
             CatStats.MaxHealth = _playerDamageables[0].MaxHealth;
             CatStats.CurrentMp = _playerDamageables[0].MP;
             CatStats.MaxMp = _playerDamageables[0].MaxMP;
             CatStats.AttackDamage = _playerAttacks[0].attackDamage;

             DogStats.CurrentHealth = _playerDamageables[1].Health;
             DogStats.MaxHealth = _playerDamageables[1].MaxHealth;
             DogStats.CurrentMp = _playerDamageables[1].MP;
             DogStats.MaxMp = _playerDamageables[1].MaxMP;
             DogStats.AttackDamage = _playerAttacks[1].attackDamage;
        }

        public static void RestorePlayerStats()
        {
            _playerDamageables[0].Health = CatStats.CurrentHealth;
            _playerDamageables[0].MaxHealth = CatStats.MaxHealth;
            _playerDamageables[0].MP = CatStats.CurrentMp;
            _playerDamageables[0].MaxMP = CatStats.MaxMp;
            _playerAttacks[0].attackDamage = CatStats.AttackDamage;
            
            _playerDamageables[1].Health = DogStats.CurrentHealth;
            _playerDamageables[1].MaxHealth = DogStats.MaxHealth;
            _playerDamageables[1].MP = DogStats.CurrentMp;
            _playerDamageables[1].MaxMP = DogStats.MaxMp;
            _playerAttacks[1].attackDamage = DogStats.AttackDamage;
        }
    }