namespace SharedGoals.Data
{
    public class DataConstants
    {
        public class Goal
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;

            public const int DescriptionMaxLength = 100;
            public const int DescriptionMinLength = 5;
        }

        public class Tag
        {
            public const int NameMaxLength = 20;
        }

        public class Creator
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
        }

        public class GoalWork
        {
            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 10;
        }

        public class User
        {
            public const int FirstNameMaxLenght = 10;
            public const int FirstNameMinLenght = 1;

            public const int LastNameMaxLenght = 15;
            public const int LastNameMinLenght = 3;
        }

        public class Comment
        {
            public const int NameMaxLenght = 20;
            public const int NameMinLenght = 1;

            public const int BodyMaxLenght = 100;
            public const int BodyMinLenght = 5;
        }
    }
}
