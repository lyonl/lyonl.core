using System;

namespace LyonL
{
    public static class Mandate
    {
        public enum ComparisonPredicate
        {
            Equal,
            Unequal,
            LessThan,
            LessThanOrEqualTo,
            GreaterThan,
            GreaterThanOrEqualTo
        }

        public enum RangeComparePredicate
        {
            ExcludeBoth,
            IncludeMinOnly,
            IncludeMaxOnly,
            IncludeBoth
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentNullException" /> if the parameter passed is null.
        /// </summary>
        /// <typeparam name="T">type of the parameter</typeparam>
        /// <param name="value">parameter value</param>
        /// <param name="paramName">parameter name</param>
        /// <returns><see cref="ArgumentNullException" /> on error.</returns>
        public static void ParameterNotNull<T>(T value, string paramName) where T : class
        {
            That(value != default(T), () => new ArgumentNullException(paramName));
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentNullException" /> if the parameter passed is empty or the null string
        /// </summary>
        /// <param name="value">parameter value</param>
        /// <param name="paramName">parameter name</param>
        /// <returns><see cref="ArgumentNullException" /> on error.</returns>
        public static void ParameterNotNullOrEmpty(string value, string paramName)
        {
            That(!string.IsNullOrWhiteSpace(value), () => new ArgumentNullException(paramName));
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentOutOfRangeException" /> if the parameter is not in the right range.
        /// </summary>
        /// <typeparam name="T">type of the parameter</typeparam>
        /// <param name="predicate">comparison operation to apply</param>
        /// <param name="value">parameter value</param>
        /// <param name="comparisonValue">comparison value</param>
        /// <param name="paramName">parameter name</param>
        /// <returns><see cref="ArgumentOutOfRangeException" /> on error.</returns>
        public static void ParameterMustBe<T>(ComparisonPredicate predicate, T value, T comparisonValue,
            string paramName)
            where T : IComparable
        {
            switch (predicate)
            {
                case ComparisonPredicate.Equal:
                    That(value.CompareTo(comparisonValue) == 0,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be equal to {comparisonValue}"));
                    break;

                case ComparisonPredicate.Unequal:
                    That(value.CompareTo(comparisonValue) != 0,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must not be equal to {comparisonValue}"));
                    break;

                case ComparisonPredicate.GreaterThan:
                    That(value.CompareTo(comparisonValue) == 1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than {comparisonValue}"));
                    break;

                case ComparisonPredicate.GreaterThanOrEqualTo:
                    That(value.CompareTo(comparisonValue) != -1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than or equal to {comparisonValue}"));
                    break;

                case ComparisonPredicate.LessThan:
                    That(value.CompareTo(comparisonValue) == -1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be less than {comparisonValue}"));
                    break;

                case ComparisonPredicate.LessThanOrEqualTo:
                    That(value.CompareTo(comparisonValue) != 1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be less than or equal to {comparisonValue}"));
                    break;
            }
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentOutOfRangeException" /> if the parameter is not in the right range.
        ///     This version defaults to including the min and max endpoints.
        /// </summary>
        /// <typeparam name="T">type of the parameter</typeparam>
        /// <param name="value">parameter value</param>
        /// <param name="minValue">mininum comparison value</param>
        /// <param name="maxValue">maximum comparison value</param>
        /// <param name="paramName">name of the parameter</param>
        /// <returns><see cref="ArgumentOutOfRangeException" /> on error.</returns>
        public static void ParameterMustBeBetween<T>(T value, T minValue, T maxValue, string paramName)
            where T : IComparable
        {
            ParameterMustBeBetween(value, minValue, maxValue, RangeComparePredicate.IncludeBoth, paramName);
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentOutOfRangeException" /> if the parameter is not in the right range.
        /// </summary>
        /// <typeparam name="T">type of the parameter</typeparam>
        /// <param name="value">parameter value</param>
        /// <param name="minValue">mininum comparison value</param>
        /// <param name="maxValue">maximum comparison value</param>
        /// <param name="predicate">value </param>
        /// <param name="paramName">name of the parameter</param>
        /// <returns><see cref="ArgumentOutOfRangeException" /> on error.</returns>
        public static void ParameterMustBeBetween<T>(T value, T minValue, T maxValue, RangeComparePredicate predicate,
            string paramName)
            where T : IComparable
        {
            ParameterMustBe(ComparisonPredicate.GreaterThanOrEqualTo, maxValue, minValue, "maxValue");
            switch (predicate)
            {
                case RangeComparePredicate.ExcludeBoth:
                    That(value.CompareTo(minValue) == 1 && value.CompareTo(maxValue) == -1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than {minValue} and less than {maxValue}"));
                    break;

                case RangeComparePredicate.IncludeMinOnly:
                    That(value.CompareTo(minValue) != -1 && value.CompareTo(maxValue) == -1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than or equal to {minValue} and less than {maxValue}"));
                    break;

                case RangeComparePredicate.IncludeMaxOnly:
                    That(value.CompareTo(minValue) == 1 && value.CompareTo(maxValue) != 1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than {minValue} and less than or equal to {maxValue}"));
                    break;

                case RangeComparePredicate.IncludeBoth:
                    That(value.CompareTo(minValue) != -1 && value.CompareTo(maxValue) != 1,
                        () =>
                            new ArgumentOutOfRangeException(paramName,
                                $"Parameter must be greater than or equal to {minValue} and less than or equal to {maxValue}"));
                    break;
            }
        }

        public static void That<TException>(bool condition, Func<TException> defer) where TException : Exception, new()
        {
            if (!condition) throw defer.Invoke();
        }
    }
}