using System;

namespace ConsoleApplication1
{
    // Код, относящийся к примеру Abstract Factory
    internal abstract class CarFactory
        // взято http://msgeeks.ru/?artid=9
        /* К недостаткам паттерна Abstract Factory следует отнести то, что при расширении возможностей фабрики путем добавления 
        нового типа продуктов придется редактировать все конкретные реализации abstract factory, а это порой бывает 
        недопустимо, например, если уже создано 100 конкретных фабрик.
        сначала создадим абстрактную фабрику carfactory, содержащую семейство из двух объектов — автомобиля и двигателя для него.*/
    {
        public abstract AbstractCar CreateCar();
        public abstract AbstractEngine CreateEngine();
    }

    internal class BMWFactory : CarFactory
        /*В результате у нас появился абстрактный класс с двумя методами, обеспечивающими получение соответствующих абстрактных объектов.
        Теперь реализуем первую конкретную фабрику, создающую класс, описывающий автомобиль BMW и двигатель для него:*/
    {
        public override AbstractCar CreateCar()
        {
            return new BMWCar();
        }

        public override AbstractEngine CreateEngine()
        {
            return new BMWEngine();
        }
    }

    internal class AudiFactory : CarFactory
        /*Сделаем то же самое для автомобиля марки Audi, чтобы у нас возникла вторая конкретная фабрика:*/
    {
        public override AbstractCar CreateCar()
        {
            return new AudiCar();
        }

        public override AbstractEngine CreateEngine()
        {
            return new AudiEngine();
        }
    }

    internal abstract class AbstractCar
        /* Теперь опишем абстрактный класс для наших автомобилей. В данном случае у них будет один метод, позволяющий узнать максимальную скорость машины.
        С его помощью мы обратимся и ко второму объекту — двигателю: */
    {
        public abstract void MaxSpeed(AbstractEngine engine);
    }

    internal abstract class AbstractEngine
        /* Все двигатели, в свою очередь, будут содержать один параметр — максимальную скорость. Эта простая общедоступная переменная позволит 
            сократить объем программы в данном примере:  */
    {
        public int MaxSpeed;
    }

    internal class BMWCar : AbstractCar
        //Реализуем класс для автомобиля BMW:
    {
        public override void MaxSpeed(AbstractEngine engine)
        {
            Console.WriteLine("Макcимальная скорость: " + engine.MaxSpeed.ToString());
        }
    }

    internal class BMWEngine : AbstractEngine
        //А затем определяем параметры его двигателя:
    {
        public BMWEngine()
        {
            MaxSpeed = 200;
        }
    }

    internal class AudiCar : AbstractCar
        //Проделаем то же самое для класса, описывающего автомобиль Audi:
    {
        public override void MaxSpeed(AbstractEngine engine)
        {
            Console.WriteLine("Макcимальная скорость: " + engine.MaxSpeed.ToString());
        }
    }

    internal class AudiEngine : AbstractEngine
        //Задаем двигатель для него:
    {
        public AudiEngine()
        {
            MaxSpeed = 180;
        }
    }

    internal class Client
        /* Теперь мы создадим класс Client, где покажем, как осуществляется работа с абстрактной фабрикой. 
    В конструктор такого класса будут передаваться все конкретные фабрики, которые и начнут создавать объекты автомобиль и двигатель.
    Следовательно, в конструктор класса Client допустимо передать любую конкретную фабрику, работающую с любыми марками автомобилей. 
    А метод Run позволит узнать максимальную скорость конкретной машины.  */
    {
        private readonly AbstractCar _abstractCar;
        private readonly AbstractEngine _abstractEngine;

        public Client(CarFactory carFactory)
        {
            _abstractCar = carFactory.CreateCar();
            _abstractEngine = carFactory.CreateEngine();
        }

        public void Run()
        {
            _abstractCar.MaxSpeed(_abstractEngine);
        }
    }

    //-----------------------------------------------------------------
    // Код, относящийся к примеру Template Method
    internal class Algorithm
        // взято http://www.dotsite.ru/solutions/patterns/TemplateMethod/
    {
        public void DoAlgorithm()
        {
            Console.WriteLine("In DoAlgorithm");
            Console.WriteLine("In Algorithm - DoAlgoStep1");
            Console.WriteLine("In Algorithm - DoAlgoStep2");
            DoAlgoStep3();
            Console.WriteLine("In Algorithm - DoAlgoStep4");
            DoAlgoStep5();
        }

        public virtual void DoAlgoStep3()
        {
            Console.WriteLine("In Algorithm - DoAlgoStep3");
        }

        public virtual void DoAlgoStep5()
        {
            Console.WriteLine("In Algorithm - DoAlgoStep5");
        }
    }

    internal class CustomAlgorithm : Algorithm
    {
        public override void DoAlgoStep3()
        {
            Console.WriteLine("In CustomAlgorithm - DoAlgoStep3");
        }

        public override void DoAlgoStep5()
        {
            Console.WriteLine("In CustomAlgorithm - DoAlgoStep5");
        }
    }

    //------------------------------------
    // Код, относящийся к примеру Bridge
    internal class Abstraction
        // Взято http://www.dotsite.ru/solutions/patterns/bridge/
    {
        protected Implementation ImpToUse;

        public void SetImplementation(Implementation i)
        {
            ImpToUse = i;
        }

        public virtual void DumpString(string str)
        {
            ImpToUse.DoStringOp(str);
        }
    }

    internal class DerivedAbstractionOne : Abstraction
    {
        public override void DumpString(string str)
        {
            str += ".com";
            ImpToUse.DoStringOp(str);
        }
    }

    internal class Implementation
    {
        public virtual void DoStringOp(string str)
        {
            Console.WriteLine("Standard implementation - print string as is");
            Console.WriteLine("string = {0}", str);
        }
    }

    internal class DerivedImplementationOne : Implementation
    {
        public override void DoStringOp(string str)
        {
            Console.WriteLine("DerivedImplementation_One - don't print string");
        }
    }

    internal class DerivedImplementationTwo : Implementation
    {
        public override void DoStringOp(string str)
        {
            Console.WriteLine("DerivedImplementation_Two - print string twice");
            Console.WriteLine("string = {0}", str);
            Console.WriteLine("string = {0}", str);
        }
    }

    public class ClientForBridge
    {
        private Abstraction SetupMyParticularAbstraction()
        {
            Abstraction a = new DerivedAbstractionOne();
            a.SetImplementation(new DerivedImplementationTwo());
            return a;
        }

        public void MainForBridge()
        {
            var c = new ClientForBridge();
            Abstraction a = c.SetupMyParticularAbstraction();
            a.DumpString("Clipcode");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Абстрактная фабрика № 1
            CarFactory bmwCar = new BMWFactory();
            var c1 = new Client(bmwCar);
            c1.Run();
            // Абстрактная фабрика № 2     
            CarFactory audiFactory = new AudiFactory();
            var c2 = new Client(audiFactory);
            c2.Run();
            Console.WriteLine("");

            //пример TemplateMethod 
            var c = new CustomAlgorithm();
            c.DoAlgorithm();
            Console.WriteLine("");

            //пример Bridge 
            var d = new ClientForBridge();
            d.MainForBridge();

            Console.Read();
        }
    }
}