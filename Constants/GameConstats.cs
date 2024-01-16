namespace GameConstants
{
    public class GameConstant
    {
        public const string MenuMsg = "1. Start a new game" + "\n0. Exit\n",
            DifficultyMenuMsg =
                "Choose the difficulty:"
                + "\n\t1.Easy: highest stats for heroes, lowest stats for monster"
                + "\n\t2.Difficult: lowest stats for heroes, highest stats for monster"
                + "\n\t3.RandomStats: Is the goddess of luck smiling upon you?"
                + "\n\t4.Personalized: personalize your heroes attributes and monster\n",
            RequestCommandMsg =
                "Insert {0}'s action"
                + "\n\t1.Normal attack"
                + "\n\t2. Character's ability"
                + "\n\t3. Guard \n",
            DefaultDifficultyMsg = "Too many attempts, default difficulty: Random\n",
            DefaultStatsMsg = "Too many attempts, assigning lowest stats\n",
            ErrorEndMsg = "Too many attempts, end of the game\n",
            EndMsg = "End of the game\n",
            CurrentStatus = "{0} : {1} Hp",
            Round = "Round {0}",
            DefaultCommandMsg = "Too many attempts, default command: attack",
            FailedAttackMsg = "{0} has failed the attack\n",
            CriticalAttackMsg = "{0} has executed a critical hit.\n",
            OnCooldown = "Skill on Cooldown, {0} turns until available\n",
            ErrorMsg = "Wrong insert, try again\n",
            RenameMsg = "Do you want rename characters:\n[Y/N]\n",
            Yes = "Y",
            No = "N",
            HpMenuMsg = "Hit Points: ",
            AttackMenuMsg = "Attack: ",
            DmgReduccionMenuMsg = "Damage  Reduction: ",
            RangedInMsg = "In range [{0}-{1}]",
            InsertRequestMsg = "Insert stat value",
            RequestValueOfStatsMsg = "Next, you will enter the stats of {0} within the specified ranges.",
        ZeroStr = "0",
            OneStr = "1",
            TwoStr = "2",
            ThreeStr = "3",
            FourStr = "4";

        public const int Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            SkillCd = 5,
                RowToSet = 2,
                MinValueRow = 0,
                MaxValueRow = 1,
                MaxAttempts = 3,
            CriticalProbability = 10,
            FailedAttackProbabilitty = 50;

        public const float ArcherMinHp = 1000f,
            ArcherMinAttack = 200f,
            ArcherMinReduction = 25f,
            ArcherMaxHp = 2000f,
            ArcherMaxAttack = 300f,
            ArcherMaxReduction = 35f,
            MonsterMinHp = 7000f,
            MonsterMinAttack = 300f,
            MonsterMinReduction = 20f,
            MonsterMaxHp = 10000f,
            MonsterMaxAttack = 400f,
            MonsterMaxReduction = 30f,
            GuardEffect = 2;
    }
}
