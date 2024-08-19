namespace iVertion.Domain.Testes
{
    public class PersonUnitTest1
    {
        [Fact(DisplayName = "Create Person With Valid Parameters")]
        public void CreatePerson_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Person(1,
                                             "Jhon",
                                             "Jacob Jhones",
                                             new DateTime(2000, 1, 1, 0, 0, 0),
                                             "SomeStringPhotografFilePathOrBase64");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact(DisplayName = "Create Oerson With Negative Id")]
        public void CreatePerson_WithNegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Person(-1,
                                             "Jhon",
                                             "Jacob Jhones",
                                             new DateTime(2000, 1, 1, 0, 0, 0),
                                             "SomeStringPhotografFilePathOrBase64");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id, must greater than zero.");
        }
    }
}
