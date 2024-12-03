/*
Легенда:
Вы - герой и у вас есть несколько умений, которые вы можете использовать против Босса. Вы должны уничтожить босса и только после этого будет вам покой. 

Формально:
Перед вами Босс, у которого есть определенное количество жизней и атака. 
Атака может быть как всегда одной и той же, так и определяться рандомом в начале раунда.
У Босса обычная атака. Босс должен иметь возможность убить героя.
У героя есть 4 умения
1. Обычная атака
2. Огненный шар, который тратит ману
3. Взрыв. Можно вызывать, только если был использован огненный шар. Для повторного применения надо повторно использовать огненный шар.
4. Лечение. Восстанавливает здоровье и ману, но не больше их максимального значения. Можно использовать ограниченное число раз.
Если пользователь ошибся с вводом команды или не выполнилось условие, то герой пропускает ход и происходит атака Босса
Программа завершается только после смерти босса или смерти пользователя, а если у вас возможно одновременно убить друг друга, то надо сообщить о ничье. 
 */
using System;
using System.Text;

namespace CSharplight
{
    internal class Programm
    {
        static void Main(string[] args)
        {
            const string CommandPlayerDamage = "1";
            const string CommandFireBall = "2";
            const string CommandExplosion = "3";
            const string CommandHealing = "4";

            Console.ForegroundColor = ConsoleColor.Green;

            int playerMaxHealth = 100;
            int playerNowHealth = 100;
            int playerMaxMana = 80;
            int playerNowMana = 80;
            int availableHealing;
            int manaRestoration;
            Random random = new Random();
            int playerDamage = random.Next(25, 35);
            int playerFireball = random.Next(30, 40);
            int manaForFireball = 30;
            int playerExplosion = random.Next(40, 50);
            int manaForExplosion = 50;
            int healingAmount = random.Next(45, 65);
            int maxAmountOfHealing = 2;
            int amountOfHealing = 0;
            int actualHeal;
            int actualMana;

            bool playerCanCastFireball = false;

            int bossHealth = 125;
            int bossDamage = random.Next(35, 50);

            while (bossHealth > 0 && playerNowHealth > 0)
            {
                Console.WriteLine($"Бой с боссом, ваше здоровье: {playerNowHealth}\nваша мана: {playerNowMana}\nумения:\n1.Обычная атака\n2.Огненный шар(тратит {manaForFireball} маны)\n3.Взрыв(Тратит {manaForExplosion}, можно вызывать, только если был использован огненный шар. Для повторного применения надо повторно использовать огненный шар)\n4.Лечение(Восстанавливает здоровье и ману, но не больше их максимального значения. Можно использовать {maxAmountOfHealing} раз)");
                Console.WriteLine($"Здоровье босса {bossHealth}");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandPlayerDamage:
                        bossHealth -= playerDamage;
                        Console.WriteLine($"Вы использовали обычную атаку и нанесли {playerDamage} урона\nВы получили {bossDamage} урона от босса");
                        break;

                    case CommandFireBall:

                        if (playerNowMana > 0 && playerNowMana >= manaForFireball)
                        {
                            bossHealth -= playerFireball;
                            playerNowMana -= manaForFireball;
                            playerCanCastFireball = true;
                            Console.WriteLine($"Вы использовали огненный шар и нанесли {playerFireball} урона\nВы получили {bossDamage} урона от босса");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно маны");
                        }
                        break;

                    case CommandExplosion:

                        if (playerNowMana > 0 && playerCanCastFireball && playerNowMana >= manaForExplosion)
                        {
                            bossHealth -= playerExplosion;
                            playerNowMana -= manaForExplosion;
                            playerCanCastFireball = false;
                            Console.WriteLine($"Вы использовали взрыв и нанесли {playerExplosion} урона\nВы получили {bossDamage} урона от босса");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно маны или не выполнены условия.");
                        }
                        break;

                    case CommandHealing:

                        if (amountOfHealing < maxAmountOfHealing && playerNowHealth < playerMaxHealth || playerNowMana < playerMaxMana)
                        {
                            availableHealing = playerMaxHealth - playerNowHealth;
                            manaRestoration = playerMaxMana - playerNowMana;
                            actualHeal = healingAmount < availableHealing ? healingAmount : availableHealing;
                            playerNowHealth += actualHeal;
                            actualMana = healingAmount < manaRestoration ? healingAmount : manaRestoration;
                            playerNowMana += actualMana;
                            amountOfHealing++;
                            Console.WriteLine($"Вы использовали лечение и восстановили {healingAmount} здоровья и маны. Вы использовали {amountOfHealing} лечения.");
                        }
                        if (amountOfHealing == maxAmountOfHealing)
                        {
                            Console.WriteLine("Вы потратили всё лечение");
                        }
                        else
                        {
                            Console.WriteLine("вам не требуется лечение");
                        }
                        break;

                    default:
                        playerNowHealth = playerMaxHealth - bossDamage;
                        Console.WriteLine("Ошибка при вводе команды или не выполнено условие,пропуск хода...\nВы получили {bossDamage} урона от босса");
                        break;

                }

                if (userInput != CommandHealing)
                {
                    playerNowHealth -= bossDamage;
                }

                if (playerNowHealth <= 0 && bossHealth <= 0)
                {
                    Console.WriteLine("герой и босс пали в бою друг с другом...");
                }
                else if (bossHealth <= 0)
                {
                    Console.WriteLine("Босс пал от руки героя!!!");
                }
                else if (playerNowHealth <= 0)
                {
                    Console.WriteLine("Герой пал от рук босса...");
                }
            }
        }
    }
}
