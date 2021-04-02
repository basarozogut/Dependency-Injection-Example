using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();
        }

        private readonly Dictionary<Type, Type> _dependencyMappings;

        public Program()
        {
            _dependencyMappings = new Dictionary<Type, Type>();
        }

        public void Start()
        {
            RegisterDependency(typeof(ProductRepository), typeof(IProductRepository));
            RegisterDependency(typeof(ProductService), typeof(IProductService));

            var productService = (IProductService)ResolveDependency(typeof(IProductService));

            var product = productService.GetProduct(1);
            PrintProduct(product);

            productService.AddProduct(new ProductDto()
            {
                Id = 1,
                Name = "Visual Studio"
            });

            var product2 = productService.GetProduct(1);
            PrintProduct(product2);

            Console.WriteLine("Completed. Press any key to exit.");
            Console.ReadLine();
        }

        private void PrintProduct(ProductDto product)
        {
            if (product == null)
            {
                Console.WriteLine("Product is null.");
            }
            else
            {
                Console.WriteLine($"Product Id: {product.Id} Product Name: {product.Name}");
            }
        }

        private void RegisterDependency(Type concreteType, Type abstractType)
        {
            _dependencyMappings[abstractType] = concreteType;
        }

        private object ResolveDependency(Type abstractType)
        {
            if (_dependencyMappings.ContainsKey(abstractType))
            {
                var concreteType = _dependencyMappings[abstractType];
                var constructor = concreteType.GetConstructors().Single();

                if (!constructor.GetParameters().Any())
                {
                    return Activator.CreateInstance(concreteType);
                }
                else
                {
                    var resolvedDependencies = new List<object>();

                    foreach (var parameterInfo in constructor.GetParameters())
                    {
                        resolvedDependencies.Add(ResolveDependency(parameterInfo.ParameterType));
                    }

                    return Activator.CreateInstance(concreteType, resolvedDependencies.ToArray());
                }
            }

            throw new Exception("Dependency mapping not found!");
        }
    }
}
