
using RPGHeroes;
using RPGHeroes.Item;
using System;
using Xunit;
using static RPGHeroes.Enum;

namespace RPGHeroesTest
{
    public class ItemTest
    {
        [Fact]
        public void Weapon_CreatedWithCorrectName()
        {
            // Arrange
            string expectedName = "Sword";

            // Act
            var weapon = new Weapon(expectedName, 1, 10, WeaponType.Sword);

            // Assert
            Assert.Equal(expectedName, weapon.Name);
        }

        [Fact]
        public void Weapon_CreatedWithCorrectRequiredLevel()
        {
            // Arrange
            int expectedRequiredLevel = 3;

            // Act
            var weapon = new Weapon("Axe", expectedRequiredLevel, 15, WeaponType.Axe);

            // Assert
            Assert.Equal(expectedRequiredLevel, weapon.RequiredLevel);
        }

        [Theory]
        [InlineData("Sword", Slot.Weapon)]
        [InlineData("Axe", Slot.Weapon)]
        [InlineData("Bow", Slot.Weapon)]
        [InlineData("Staff", Slot.Weapon)]
        public void Weapon_CreatedWithCorrectSlot(string name, Slot expectedSlot)
        {
            // Act
            var weapon = new Weapon(name, 1, 10, WeaponType.Sword);

            // Assert
            Assert.Equal(expectedSlot, weapon.Slot);
        }

        [Theory]
        [InlineData("Sword", WeaponType.Sword)]
        [InlineData("Axe", WeaponType.Axe)]
        [InlineData("Bow", WeaponType.Bow)]
        [InlineData("Staff", WeaponType.Staff)]
        public void Weapon_CreatedWithCorrectWeaponType(string name, WeaponType expectedWeaponType)
        {
            // Act
            var weapon = new Weapon(name, 1, 10, expectedWeaponType);

            // Assert
            Assert.Equal(expectedWeaponType, weapon.GetWeaponType);
        }

        [Fact]
        public void Weapon_CreatedWithCorrectDamage()
        {
            // Arrange
            int expectedDamage = 20;

            // Act
            var weapon = new Weapon("Dagger", 2, expectedDamage, WeaponType.Dagger);

            // Assert
            Assert.Equal(expectedDamage, weapon.WeaponDamage);
        }
    }
}


