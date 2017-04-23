# DapperExtensions.Sample
the Sample of DapperExtensions 

项目主要包含
1、MvcApplication1 有若干通过httpclient调用WEBAPI的例子，
2、Test.API  为WEBAPI服务提供端，简单封装了身份校验，错误捕获，统一json返回值等操作的filter的例子
3、Test.BLL 业务逻辑封装例子
4、Test.Cache 底层依赖于开源项目 WebApi.OutputCache.Core，Enyim.Caching，ServiceStack，封装了针对单机缓存的应用程序池缓存，多机的redis和memcached
5、Test.DAL  底层数据操作的例子，包含事务处理。
6、Test.Entity 实体层
7、Test.Utility 通用类，编码解码，校验，日志等
8、WebApplication1 直接调用BLL-->DAL的例子，不走webAPI，即针对不需要做分布式处理的应用程序
