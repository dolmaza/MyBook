using Core.Properties;
using System.Collections.Generic;

namespace Core.Validation
{
    public class CustomError
    {
        #region Properties
        public int Code { get; set; }
        public int SubCode { get; set; }
        public string ErrorMessage { get; set; }
        #endregion Properties
    }

    public class CustomErrors
    {
        public static CustomError FirstnameRequired { get; set; } = new CustomError { Code = 1, ErrorMessage = Resources.ValidationFirstnameRequired };
        public static CustomError LastnameRequired { get; set; } = new CustomError { Code = 2, ErrorMessage = Resources.ValidationLastnameRequired };
        public static CustomError AddressRequired { get; set; } = new CustomError { Code = 3, ErrorMessage = Resources.ValidationAddressRequired };
        public static CustomError MobileRequired { get; set; } = new CustomError { Code = 4, ErrorMessage = Resources.ValidationMobileRequired };
        public static CustomError DeliveryTimeRequired { get; set; } = new CustomError { Code = 5, ErrorMessage = Resources.ValidationDeliveryTimeRequired };
    }

    public class ValidationBase
    {
        #region Methods

        public static CustomError ValidateFirstname(string firstname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
            {
                return CustomErrors.FirstnameRequired;
            }

            return null;
        }

        public static CustomError ValidateLastname(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname))
            {
                return CustomErrors.LastnameRequired;
            }

            return null;
        }

        public static CustomError ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return CustomErrors.AddressRequired;
            }

            return null;
        }

        public static CustomError ValidateMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return CustomErrors.MobileRequired;
            }

            return null;
        }

        public static CustomError ValidateDeliveryTime(string deliveryTime)
        {
            if (string.IsNullOrWhiteSpace(deliveryTime))
            {
                return CustomErrors.DeliveryTimeRequired;
            }
            return null;
        }

        #endregion Methods
    }

    public class Validation : ValidationBase
    {
        public static List<CustomError> ValidateOrderEditForm(string firstname, string lastname, string address, string mobile, string deliveryTime)
        {
            var errors = new List<CustomError>
            {
                ValidateFirstname(firstname),
                ValidateLastname(lastname),
                ValidateAddress(address),
                ValidateMobile(mobile),
                ValidateDeliveryTime(deliveryTime)
            };

            errors.RemoveAll(e => e == null);

            return errors;
        }
    }
}
