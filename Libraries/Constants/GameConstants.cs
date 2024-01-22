namespace GameConstants
{
    public class GameConstant
    {
        public const string AttackMenuMsg = "Attack: ",
            CriticalAttackMsg = "\n{0} has executed a critical hit.",
            CurrentStatus = "\n{0} : {1} Hp",
            DefaultCommandMsg = "\nToo many attempts, default command: attack",
            DefaultDifficultyMsg = "\nToo many attempts, default difficulty: Random",
            DefaultHeroStatsMsg = "\nToo many attempts, assigning lowest stats",
            DefaultMonsterStatsMsg = "\nToo many attempts, assigning highest stats",
            DifficultyEasy = "1",
            DifficultyDifficult = "2",
            DifficultyPersonalized = "4",
            DifficultyMenuMsg =
                "\nChoose the difficulty:"
                + "\n\t1.Easy: highest stats for heroes, lowest stats for monster"
                + "\n\t2.Difficult: lowest stats for heroes, highest stats for monster"
                + "\n\t3.RandomStats: Is the goddess of luck smiling upon you?"
                + "\n\t4.Personalized: personalize your heroes attributes and monster",
            DmgReductionMenuMsg = "Damage  Reduction: ",
            EndMsg = "End of the game\n",
            ErrorEndMsg = "\nToo many attempts, end of the game0",
            ErrorMsg = "\nWrong insert, try again",
            FailedAttackMsg = "\n{0} has failed the attack",
            FourStr = "4",
            HpMenuMsg = "Hit Points: ",
            InsertRequestMsg = "Insert stat value",
            MenuMsg = "1. Start a new game" + "\n0. Exit",
            No = "N",
            OnCooldown = "\nSkill on Cooldown, {0} turns until available",
            OneStr = "1",
            RangedInMsg = "In range [{0}-{1}]",
            RenamedMsg = "\n{0}'s new name is {1}",
            RenameMsg = "Do you want rename characters:\n[Y/N]\n",
            RequestCommandMsg =
                "\nInsert {0}'s action"
                + "\n\t1.Normal attack"
                + "\n\t2. Character's ability"
                + "\n\t3. Guard \n",
            RequestNameMsg = "\nInsert {0}'s new name is ? ",
            RequestValueOfStatsMsg =
                "\nNext, you will enter the stats of {0} within the specified ranges.",
            Round = "\nRound {0}",
            ThreeStr = "3",
            TwoStr = "2",
            Yes = "Y",
            ZeroStr = "0",
            MonsterName = "Monster",
            ArcherName = "Archer",
            ArcherAbility = "\n{0} has stunned {1} for {2} turns",
            BarbarianName = "Barbarian",
            BarbarianAbility = "\n{0} is immune to damage for {1} turns",
            MageName = "Mage",
            MageAbility = "\n{0} has dealt {1} damage to {2}",
            DruidName = "Druid",
            DruidAbility = "\n{0} has healed {1} {2} hp",
            AttackMsg = "\n{0} has dealt {1} damage to {2}",
            MonsterTurnMsg = "\n{0} is about to attack next",
            MonsterStunnedMsg = "\n{0} is stunned"
            ;


        public const int Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Hundred=100,
            FiveHundred = 500,
            SkillCd = 5,
            RowsIteration = 1,
            RowToSetMaxValues = 2,
            RowCurrentValues = 3,
            MinValueRow = 0,
            MaxValueRow = 1,
            MaxAttempts = 3,
            CriticalProbability = 10,
            FailedAttackProbability = 5,
            HpValueColumn = 0,
            MaxStatsRow = 2,
            AttackValueColumn = 1,
            GuardEffect = 2,
            ReductionValueColumn = 2,
            BarbarianSkillDuration=2;

        public const float ArcherSystemLimitMinHp = 1500f,
            ArcherSystemLimitMinAttack = 200f,
            ArcherSystemLimitMinReduction = 25f,
            ArcherSystemLimitMaxHp = 2000f,
            ArcherSystemLimitMaxAttack = 300f,
            ArcherSystemLimitMaxReduction = 35f,
            BarbarianSystemLimitMinHp = 3000f,
            BarbarianSystemLimitMinAttack = 150f,
            BarbarianSystemLimitMinReduction = 35f,
            BarbarianSystemLimitMaxHp = 3750f,
            BarbarianSystemLimitMaxAttack = 250f,
            BarbarianSystemLimitMaxReduction = 45f,
            MageSystemLimitMinHp = 1100f,
            MageSystemLimitMinAttack = 300f,
            MageSystemLimitMinReduction = 20f,
            MageSystemLimitMaxHp = 1500f,
            MageSystemLimitMaxAttack = 400f,
            MageSystemLimitMaxReduction = 35f, 
            DruidSystemLimitMinHp = 2000f,
            DruidSystemLimitMinAttack = 70f,
            DruidSystemLimitMinReduction = 25f,
            DruidSystemLimitMaxHp = 2500f,
            DruidSystemLimitMaxAttack = 120f,
            DruidSystemLimitMaxReduction = 40f,
            MonsterSystemLimitMinHp = 7000f,
            MonsterSystemLimitMinAttack = 300f,
            MonsterSystemLimitMinReduction = 20f,
            MonsterSystemLimitMaxHp = 10000f,
            MonsterSystemLimitMaxAttack = 400f,
            MonsterSystemLimitMaxReduction = 30f;
            
    }
}
