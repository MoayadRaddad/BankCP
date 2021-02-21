(function ($) {

    jQuery.validator.addMethod('minimumservicetime', function (value, element, params) {
        if (!/Invalid|NaN/.test(value)) {
            var $property = $('#' + params.maximumservicetime);
            var propertyValue = $property.val();
            return parseInt(value) < parseInt(propertyValue);
        }
        return isNaN(value) && isNaN($(params).val()) || (parseFloat(value) > parseFloat($(params).val()));
    }, '');

    jQuery.validator.unobtrusive.adapters.add('minimumservicetime', ['maximumservicetime'], function (options) {
        options.rules['minimumservicetime'] = {
            maximumservicetime: options.params.maximumservicetime
        };
        options.messages['minimumservicetime'] = options.message;
    });


    jQuery.validator.addMethod('maximumservicetime', function (value, element, params) {
        if (!/Invalid|NaN/.test(value)) {
            var $property = $('#' + params.minimumservicetime);
            var propertyValue = $property.val();
            return parseInt(value) > parseInt(propertyValue);
        }
        return isNaN(value) && isNaN($(params).val()) || (parseFloat(value) > parseFloat($(params).val()));
    }, '');

    jQuery.validator.unobtrusive.adapters.add('maximumservicetime', ['minimumservicetime'], function (options) {
        options.rules['maximumservicetime'] = {
            minimumservicetime: options.params.minimumservicetime
        };
        options.messages['maximumservicetime'] = options.message;
    });
})(jQuery);