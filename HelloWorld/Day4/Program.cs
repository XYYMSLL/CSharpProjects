using System;

namespace Day4
{

    struct MyStruct
    {
        public MyStruct(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }
        public int Age { get; }

        public override string ToString()
        {
            return Name + ", " + Age;
        }

        public bool Equals(MyStruct other)
        {
            return other.Name == Name;
        }
    }

    class Dog
    {
        private string _Name = "";
        private string _Kind = "";
        private int _Age = -1;
        private string _nickname = "";

        public string Nickname
        {
            get
            {
                return _nickname;
            }
            set
            {
                _nickname = value;
            }
        }

        public Dog()
        {
        }

        public Dog(string Name, string Kind, int Age)
        {
            _Name = Name;
            _Kind = Kind;
            _Age = Age;
        }

        public void SetName(string Name)
        {
            _Name = Name;
        }
        public bool SetKind(string Kind)
        {
            if (string.IsNullOrEmpty(_Kind))
            {
                _Kind = Kind;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetKind(int Age)
        {
            if (_Age == -1)
            {
                _Age = Age;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetName()
        {
            return _Name;
        }
        public string GetKind()
        {
            return _Kind;
        }
        public int GetAge()
        {
            return _Age;
        }

        // override
        public override int GetHashCode()
        {
            return (_Name, _Kind, _Age).GetHashCode();
        }
        public override bool Equals(Object obj)
        {
            return Equals(obj as Dog);
        }

        // implement Equals and ==, !=
        public bool Equals(Dog otherDog)
        {
            if (Object.ReferenceEquals(this, otherDog))
            {
                return true;
            }

            return _Name == otherDog.GetName() && _Kind == otherDog.GetKind() && _Age == otherDog.GetAge();
        }

        public static bool operator ==(Dog leftDog, Dog rightDog)
        {
            return leftDog.Equals(rightDog);
        }
        public static bool operator !=(Dog leftDog, Dog rightDog)
        {
            return !leftDog.Equals(rightDog);
        }
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            //Console.Write(typeof(string).Assembly.ImageRuntimeVersion);

            Dog dog1 = new Dog("张三", "A", 1);
            Dog dog2 = new Dog("李四", "A", 1);


            dog1.Nickname = "小五";
            string dog1nickname = dog1.Nickname;

            Console.WriteLine(dog1nickname);
        }
    }

    //class CppClass
    //{
    //    private:
    //        //private variables and methods
    //        int age;
    //        int phoneNum;

    //    public:
    //        //public variables and methods
    //        CppClass() { }
    //        bool Foo() { }
    //}
}
