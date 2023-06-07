using RPGHeroes;
using System;
using Xunit;

namespace RPGHeroesTest
{
    public class HeroTest
    {

        [Fact]
        public void HeroCreation_ReturnCorrectValues_Mage()
        {
            string expectedName = "TestHero";
            int expectedLevel = 1;
            int expectedStrength = 1;
            int expectedDexterity = 1;
            int expectedIntelligence = 8;

            // Act
            Hero hero = new Mage(expectedName);

            // Assert
            Assert.Equal(expectedName, hero.Name);
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }

        [Fact]
        public void HeroCreation_ReturnCorrectValues_Ranger()
        {
            string expectedName = "TestHero";
            int expectedLevel = 1;
            int expectedStrength = 1;
            int expectedDexterity = 7;
            int expectedIntelligence = 1;

            // Act
            Hero hero = new Ranger(expectedName);

            // Assert
            Assert.Equal(expectedName, hero.Name);
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }


        [Fact]
        public void HeroCreation_ReturnCorrectValues_Rogue()
        {
            string expectedName = "TestHero";
            int expectedLevel = 1;
            int expectedStrength = 2;
            int expectedDexterity = 6;
            int expectedIntelligence = 1;

            // Act
            Hero hero = new Rogue(expectedName);

            // Assert
            Assert.Equal(expectedName, hero.Name);
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }
        [Fact]
        public void HeroCreation_ReturnCorrectValues_Warrior()
        {
            string expectedName = "TestHero";
            int expectedLevel = 1;
            int expectedStrength = 5;
            int expectedDexterity = 2;
            int expectedIntelligence = 1;

            // Act
            Hero hero = new Warrior(expectedName);

            // Assert
            Assert.Equal(expectedName, hero.Name);
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }

        [Fact]
        public void HeroLevelUp_Mage()
        {

            int expectedStrength = 1 + 1;
            int expectedDexterity = 1 + 1;
            int expectedIntelligence = 8 + 5;

            // Act
            Hero hero = new Mage("name");
            hero.LevelUp();


            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }
        [Fact]
        public void HeroLevelUp_Ranger()
        {
            //assert
            int expectedStrength = 1 + 1;
            int expectedDexterity = 7 + 5;
            int expectedIntelligence = 1 + 1;

            Hero hero = new Ranger("name");
            hero.LevelUp();


            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }
        [Fact]
        public void HeroLevelUp_Rogue()
        {
            //assert
            int expectedStrength = 2 + 1;
            int expectedDexterity = 6 + 4;
            int expectedIntelligence = 1 + 1;

            Hero hero = new Rogue("name");
            hero.LevelUp();


            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }
        [Fact]
        public void HeroLevelUp_Warrior()
        {
            //assert
            int expectedStrength = 5 + 3;
            int expectedDexterity = 2 + 2;
            int expectedIntelligence = 1 + 1;

            Hero hero = new Warrior("name");
            hero.LevelUp();


            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }

        [Fact]
        public void WeaponCreation_ReturnCorrectValues()
        {
            string expectedName = "TestHero";
            int expectedLevel = 1;
            int expectedStrength = 1;
            int expectedDexterity = 1;
            int expectedIntelligence = 8;

            // Act
            Hero hero = new Mage(expectedName);

            // Assert
            Assert.Equal(expectedName, hero.Name);
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedStrength, hero.HeroAttributes.Strength);
            Assert.Equal(expectedDexterity, hero.HeroAttributes.Dexterity);
            Assert.Equal(expectedIntelligence, hero.HeroAttributes.Intelligence);
        }

    }
}
