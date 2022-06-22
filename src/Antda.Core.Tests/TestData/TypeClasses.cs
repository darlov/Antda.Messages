namespace Antda.Core.Tests.TestData;


public interface IMessage
{
}

public interface IMessage<TMessage> : IMessage
{
}

public class Message : IMessage
{
}

public class Message<TMessage> : IMessage<TMessage>
{
}

public class ClassWithGenericInterface : IMessage<string>
{
}

public class ClassWithNonGenericClass : Message
{
}


public class ClassWithGenericClass : Message<string>
{
}

public class ClassWithGenericMultipleInterface : IMessage<string>, IMessage<int>
{
}

public class ClassWithGenericClass2 : ClassWithGenericClass
{
}

public class ClassWithGenericClass3 : ClassWithGenericClass2
{
}



