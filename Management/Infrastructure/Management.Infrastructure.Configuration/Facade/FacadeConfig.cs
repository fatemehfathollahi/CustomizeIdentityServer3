using AutoMapper;
using AutoMapper.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Contracts.Services;
using Framework.Crosscutting.Exceptions;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Facade.ObjectMapper;

namespace Management.Infrastructure.Configuration.Facade
{
	internal class FacadeConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<IMapper>().UsingFactoryMethod(() =>
				{
					MapperConfigurationExpression oMapperConfiguration = new MapperConfigurationExpression();
					MapperConfigurations.MapperInitialize(oMapperConfiguration);

					return new MapperConfiguration(oMapperConfiguration).CreateMapper();
				}).LifestyleSingleton(),

				//Classes.FromAssemblyContaining<IClientFacadeService>()
				//	.BasedOn<IFacadeService>().WithService.FromInterface()
				//	.Configure(a => a.Interceptors<ExceptionInterceptor>())
				//	.LifestyleBoundTo<ApiController>(),

				Classes.FromAssemblyContaining<IClientFacadeService>()
					.BasedOn<IFacadeService>().WithService.FromInterface()
					.Configure(a => a.Interceptors<ExceptionInterceptor>())
					.LifestylePerWebRequest()
		   );
		}
	}
}