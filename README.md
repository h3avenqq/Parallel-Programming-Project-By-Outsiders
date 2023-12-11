# Outsiders
Репозиторий с лабораторными работами по предмету: *Методы и средства проектирования информационных систем и технологий*.

[![codecov](https://codecov.io/gh/h3avenqq/Parallel-Programming-Project-By-Outsiders/graph/badge.svg?token=C40P4RRG8Y)](https://codecov.io/gh/h3avenqq/Parallel-Programming-Project-By-Outsiders)
![GitHub top language](https://img.shields.io/github/languages/top/h3avenqq/Parallel-Programming-Project-By-Outsiders#readme)


<div align="center">
  <img src="https://liquipedia.net/commons/images/9/9b/Outsiders_CSGO_allmode.png"/>
</div>

# Содержание
- [Участники](#Участники)
- [Лабораторные](#лабораторные)
  - [Лабораторная работа №1](#лабораторная-работа-№1-параллельные-операции)
    - [Задание](#задание-о-философах)
  - [Лабораторная работа №2](#лабораторная-работа-№2-openmp)
    - [Задание](#задание-прямоугольники)
  - [Лабораторная работа №3](#лабораторная-работа-№3-mpi)
    - [Задание](#задание-трус-не-играет-в-хоккей)
- [Тесты](#тесты)
  - [Тест лаб. №1](#тест-лаб-№1)
  - [Тест лаб. №1](#тест-лаб-№2)
  - [Тест лаб. №1](#тест-лаб-№3)





## Участники
- [Ким Кирилл](https://github.com/FIRExxxWOLF)

- [Куниевский Валерий](https://github.com/VVoron)

- [Шмарев Максим](https://github.com/h3avenqq)




## Лабораторные
### [Лабораторная работа №1](./ParallelLabs.Lab1/Program.cs) (Параллельные операции)
#### Задание: О философах
`Пять философов сидят возле круглого стола. Они проводят жизнь, чередуя приемы пищи и
размышления. В центре стола находится большое блюдо спагетти. Спагетти длинные и
запутанные, чтобы съесть порцию, философу необходимо воспользоваться двумя вилками. К
несчастью, философам дали только пять вилок. Между каждой парой философов лежит одна
вилка, они договорились, что каждый будет пользоваться только теми вилками, которые лежат
рядом с ним (слева и справа). Написать программу, моделирующую действие философов.
Программа должна избегать неудачной ситуации, когда все голодны, но ни один из них не может
взять обе вилки – например, когда каждый из них держит по одной вилке и не хочет отдавать её.`

### [Лабораторная работа №2](./ParallelLabs.Lab2/Program.cs) (OpenMP)
#### Задание: Прямоугольники
`Написать программу, которая приближенно вычисляет определенный интеграл по
формуле прямоугольников. Подъинтегральную функцию задавать в виде отдельной функции, чтобы
программа могла считать разные интегралы. Проверить на различных функциях (не менее 10 тестов). Для
распараллеливания использовать OpenMP, использовать весь ресурс параллелизма (параллелить, где
можно) и оптимизации компилятора. Построить график зависимости ускорения многопоточной
программыпо сравнению с 1 потоком и сравнить с линейным ускорением.`
### [Лабораторная работа №3](./ParallelLabs.Lab3/Program.cs) (MPI)
#### Задание: Трус не играет в хоккей
`Напишите программу с использованием MPI, моделирующую ситуацию: 12
хоккеистов, по 6 в каждой из двух команд (процессы), обмениваются шайбой случайным образом. С
вероятностью 65% шайба передается игроку своей команды, с вероятностью 35% – игроку команды
противника. После 4 удачных передач шайбы (игрокам своей команды), тот хоккеист, у которого шайба
находится в данный момент, поражает ворота противника. После поражения ворот шайба передается
игроку пропустившей шайбу команды. Вывести счет матча после 2000 передач.`


### [Тесты](./ParallelLabs.Tests)
- #### [Тест лаб. №1](./ParallelLabs.Tests/PhilosopherTests.cs)
  Тест конструктора вилок:
  ```C#
    public void ForkCtor_Success()
        {
            // Arrange
            int id = 1;
            bool inUsage = false;

            // Act
            var fork = new Fork(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(id, fork.Id);
                Assert.Equal(inUsage, fork.InUsage);
            });
        }
  ```
  Тест функции `Eating`:
  ```C#
  public void PhilosopherEating_Test()
        {
            // Arrange
            int id = 2;
            var forks = new List<Fork>()
            {
                new Fork(2),
                new Fork(3)
            };
            bool wellFed = false;
            var rnd = new Random();
            var obj = new object();

            // Act
            var phil = new Philosopher(id, forks, rnd);
            phil.Eating(obj);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(!wellFed, phil.WellFed);
                Assert.Equal(forks, phil.Forks);
            });
        }
  ```
  Тест конструктора на приём пустых значений:
  ```C#
  public void PhilosopherCtor_FailOnEmptyForksList()
        {
            // Arrange
            int id = 1;
            List<Fork> forks1 = null;
            List<Fork> forks2 = new();
            var rnd = new Random();

            // Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => new Philosopher(id, forks1, rnd));
                Assert.Throws<ArgumentException>(() => new Philosopher(id, forks2, rnd));
            });
        }
  ```
  Тест конструктора философов:
  ```C#
  public void PhilosopherCtor_Success()
        {
            // Arrange
            int id = 1;
            var forks = new List<Fork>()
            {
                new Fork(1),
                new Fork(2),
                new Fork(3),
                new Fork(4)
            };
            bool wellFed = false;
            var rnd = new Random();

            // Act
            var phil = new Philosopher(id, forks, rnd);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(id, phil.Id);
                Assert.Equal(forks, phil.Forks);
                Assert.Equal(wellFed, phil.WellFed);
            });
        }
  ```

- #### [Тест лаб. №2](./ParallelLabs.Tests/IntegralTests.cs)
  Пример теста вычисления интегралла:
  ```C#
  public void IntegralF1_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F1, a, b, n, numThreads);
            var expectedResult = 0.32835;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }
  ```

- #### [Тест лаб. №3]()
  Пример теста:
  ```C#
  
  ```
