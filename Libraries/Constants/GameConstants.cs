namespace GameConstants
{
    public class GameConstant
    {
        public const string AttackMenuMsg = "Attack: ",
            CriticalAttackMsg = "{0} has executed a critical hit.\n",
            CurrentStatus = "{0} : {1} Hp",
            DefaultCommandMsg = "Too many attempts, default command: attack",
            DefaultDifficultyMsg = "Too many attempts, default difficulty: Random\n",
            DefaultHeroStatsMsg = "Too many attempts, assigning lowest stats\n",
            DefaultMonsterStatsMsg = "Too many attempts, assigning highest stats\n",
            DifficultyEasy="1",
            DifficultyDificult="2",
            DifficultyMenuMsg =
                "Choose the difficulty:"
                + "\n\t1.Easy: highest stats for heroes, lowest stats for monster"
                + "\n\t2.Difficult: lowest stats for heroes, highest stats for monster"
                + "\n\t3.RandomStats: Is the goddess of luck smiling upon you?"
                + "\n\t4.Personalized: personalize your heroes attributes and monster\n",
            DmgReductionMenuMsg = "Damage  Reduction: ",
            EndMsg = "End of the game\n",
            ErrorEndMsg = "Too many attempts, end of the game\n",
            ErrorMsg = "Wrong insert, try again\n",
            FailedAttackMsg = "{0} has failed the attack\n",
            FourStr = "4",
            HpMenuMsg = "Hit Points: ",
            InsertRequestMsg = "Insert stat value",
            MenuMsg = "1. Start a new game" + "\n0. Exit\n",
            No = "N",
            OnCooldown = "Skill on Cooldown, {0} turns until available\n",
            OneStr = "1",
            RangedInMsg = "In range [{0}-{1}]",
            RenamedMsg = "{0}'s new name is {1}",
            RenameMsg = "Do you want rename characters:\n[Y/N]\n",
            RequestCommandMsg =
                "Insert {0}'s action"
                + "\n\t1.Normal attack"
                + "\n\t2. Character's ability"
                + "\n\t3. Guard \n",
            RequestNameMsg = "Insert {0}'s new name is ",
            RequestValueOfStatsMsg =
                "Next, you will enter the stats of {0} within the specified ranges.",
            Round = "Round {0}",
            ThreeStr = "3",
            TwoStr = "2",
            Yes = "Y",
            ZeroStr = "0",
            MonsterName = "Monster",
            ArcherName = "Archer";

        public const int Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
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
            ReductionValueColumn = 2;

        public const float ArcherSystemLimitMinHp = 1000f,
            ArcherSystemLimitMinAttack = 200f,
            ArcherSystemLimitMinReduction = 25f,
            ArcherSystemLimitMaxHp = 2000f,
            ArcherSystemLimitMaxAttack = 300f,
            ArcherSystemLimitMaxReduction = 35f,
            MonsterSystemLimitMinHp = 7000f,
            MonsterSystemLimitMinAttack = 300f,
            MonsterSystemLimitMinReduction = 20f,
            MonsterSystemLimitMaxHp = 10000f,
            MonsterSystemLimitMaxAttack = 400f,
            MonsterSystemLimitMaxReduction = 30f,
            GuardEffect = 2;
    }
}
