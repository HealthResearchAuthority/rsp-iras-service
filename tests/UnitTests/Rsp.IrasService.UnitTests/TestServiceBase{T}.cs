﻿namespace Rsp.IrasService.UnitTests;

public class TestServiceBase<T> : TestServiceBase where T : class
{
    public TestServiceBase()
    {
        // creating instance this way will inject
        // a default mocked instances of all the dependencies
        // which can be retreived by calling Mocker.GetMock<>
        // e.g. Mocker.GetMock<ICategoriesService>() will return the injected instance for ICategoriesService
        Sut = Mocker.CreateInstance<T>();
    }

    public T Sut { get; set; }
}