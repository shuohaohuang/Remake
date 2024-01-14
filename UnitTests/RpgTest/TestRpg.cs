using Rpg;
namespace RpgTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestA_StatSetter()
        {
            //Arrange

            int difficulty = 1;

            float[,] archer = new float[3, 3]
            {
                { 1000f, 30f, 10f },
                { 1500f, 50f, 20f },
                { 0, 0, 0 }
            };
            float[,] expected= new float[3, 3]
            {
                { 1000f, 30f, 10f },
                { 1500f, 50f, 20f },
                { 1500f, 50f, 20f }
            };
            bool isShero = true;

            //Act
            float[,] result = SetStat.StatSetter(archer, difficulty, isShero);

            //Assert
            CollectionAssert.AreEqual(expected, archer);

        }

        [TestMethod]
        public void TestB_StatSetter()
        {
            //Arrange

            int difficulty = 1;

            float[,] archer = new float[3, 3]
            {
                { 1000f, 30f, 10f },
                { 1500f, 50f, 20f },
                { 0, 0, 0 }
            };
            float[,] expected = new float[3, 3]
            {
                { 1000f, 30f, 10f },
                { 1500f, 50f, 20f },
                { 1000f, 30f, 10f }
            };
            bool isShero = false;

            //Act
            float[,] result = SetStat.StatSetter(archer, difficulty, isShero);

            //Assert
            CollectionAssert.AreEqual(expected, archer);

        }

        [TestMethod]
        public void CalculateDamage_naturals(){

            //Arrange
            float attackerAd = 100;
            float defenderReduction = 20;
            
            //Act
            float result = Battle.CalculateDamage(attackerAd, defenderReduction,true);

            //Assert
            Assert.AreEqual(80, result);
        }
        [TestMethod]
        public void CalculateDamage_Negative()
        {
            //Arrange
            float attackerAd = -100;
            float defenderReduction = 20;

            //Act
            float result = Battle.CalculateDamage(attackerAd, defenderReduction, true);

            //Assert
            Assert.AreEqual(80, result);
        }

        [TestMethod]
        public void CalculateDamage_Positive_True()
        {
            //Arrange
            float attackerAd = 100;
            float defenderReduction = 20;
            float guardeEffect = 2;
            bool isGuarding = true;

            //Act
            float result = Battle.CalculateDamage(attackerAd, defenderReduction,guardeEffect,isGuarding,true);

            //Assert
            Assert.AreEqual(60, result);
        }
        [TestMethod]
        public void CalculateDamage_10_False()
        {
            //Arrange
            float attackerAd = 10;//Limit
            float defenderReduction = 40;
            float guardEffect = 2;
            bool isGuarding = false;

            //Act
            float result = Battle.CalculateDamage(attackerAd, defenderReduction, guardEffect, isGuarding, true);

            //Assert
            Assert.AreEqual(6, result);
        }
        /*calculateDamage
         * attackerAd minim value =10
         * because under 10 float type can't support it
         */


    }
}