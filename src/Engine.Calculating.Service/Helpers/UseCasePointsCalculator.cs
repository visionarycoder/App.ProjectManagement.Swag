using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Calculating.Service.Helpers
{
    public class UseCasePointsCalculator
    {
        public int CalculateUUCP(List<UseCase> useCases, List<Actor> actors)
        {
            int totalUseCasePoints = 0;
            foreach (var useCase in useCases)
            {
                totalUseCasePoints += useCase.ComplexityPoints;
            }

            int totalActorPoints = 0;
            foreach (var actor in actors)
            {
                totalActorPoints += actor.ComplexityPoints;
            }

            return totalUseCasePoints + totalActorPoints;
        }

        public double CalculateTCF(List<TechnicalFactor> technicalFactors)
        {
            int sumTechnicalFactors = 0;
            foreach (var factor in technicalFactors)
            {
                sumTechnicalFactors += factor.Rating;
            }
            return 0.6 + (0.01 * sumTechnicalFactors);
        }

        public double CalculateEF(List<EnvironmentalFactor> environmentalFactors)
        {
            int sumEnvironmentalFactors = 0;
            foreach (var factor in environmentalFactors)
            {
                sumEnvironmentalFactors += factor.Rating;
            }
            return 1.4 + (-0.03 * sumEnvironmentalFactors);
        }

        public double CalculateUCP(int uucp, double tcf, double ef)
        {
            return uucp * tcf * ef;
        }
    }
}
