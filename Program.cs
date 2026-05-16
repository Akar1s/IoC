using IoC_Containers;

var ioc = new IoCContainer();

ioc.Register<IEmailSender, EmailSender>(Lifetime.Singleton);
ioc.Register<ISmsAPI, SmsAPI>(Lifetime.Transient);
ioc.Register<IRequestContext, RequestContext>(Lifetime.Scoped);

var s1 = ioc.Resolve<IEmailSender>();
var s2 = ioc.Resolve<IEmailSender>();
Console.WriteLine(ReferenceEquals(s1, s2));


var t1 = ioc.Resolve<ISmsAPI>();
var t2 = ioc.Resolve<ISmsAPI>();
Console.WriteLine(ReferenceEquals(t1, t2));

using (var scope1 = ioc.CreateScope())
{
    var ctx_a = scope1.Resolve<IRequestContext>();
    var ctx_b = scope1.Resolve<IRequestContext>();
    Console.WriteLine(ctx_a.RequestId);
    Console.WriteLine(ctx_b.RequestId);
    Console.WriteLine(ReferenceEquals(ctx_a, ctx_b));

    using (var scope2 = ioc.CreateScope())
    {
        var ctx_c = scope2.Resolve<IRequestContext>();
        Console.WriteLine(ctx_c.RequestId);

    }
}