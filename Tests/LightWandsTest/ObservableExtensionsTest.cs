using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reactive.Subjects;
using System.Collections.Generic;
using Kirinji.LightWands;
using System.Reactive.Linq;

namespace LightWandsTest
{
    [TestClass]
    public class ObservableExtensionsTest
    {
        [TestMethod]
        public void UseObserverTest1()
        {
            var subject = new Subject<string>();
            var result = new List<Tuple<int, string>>();
            subject
                .Select(x => new Tuple<int, string>(0, x))
                .UseObserver((value, i, observer) =>
                {
                    observer.OnNext(new Tuple<int, string>(i, value.Item2));
                },
                null,
                observer =>
                {
                    observer.OnNext(new Tuple<int, string>(-1, "end"));
                })
                .Subscribe(result.Add);
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnNext("c");
            subject.OnCompleted();
            result.Is(
                new Tuple<int, string>(0, "a"),
                new Tuple<int, string>(1, "b"), 
                new Tuple<int, string>(2, "c"),
                new Tuple<int, string>(-1, "end"));
        }

        [TestMethod]
        public void UseObserverTest2()
        {
            var subject = new Subject<string>();
            var result = new List<string>();
            subject
                .UseObserver((value, i, observer) =>
                {
                    observer.OnNext(value);
                },
                (error, observer) =>
                {
                    observer.OnNext(error.Message);
                },
                observer =>
                {
                    observer.OnNext("end");
                })
                .Subscribe(result.Add);
            subject.OnNext("a");
            subject.OnError(new Exception("error!"));
            subject.OnCompleted();
            result.Is("a", "error!");
        }

        [TestMethod]
        public void TakeErrorTest()
        {
            var subject = new Subject<string>();
            var result = new List<IValueOrError<string>>();
            subject
                .TakeError()
                .Subscribe(result.Add);
            subject.OnNext("a");
            subject.OnError(new Exception("error!"));
            result.Count.Is(2);
            result[0].IsError.IsFalse();
            result[1].IsError.IsTrue();
        }
    }
}
