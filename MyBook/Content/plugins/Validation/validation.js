var validation = {
    errorsJson: null,
    type: 'Admin',
    types: {
        admin: 'Admin'
    },

    init: function(options) {
        if (options) {
            validation.errorsJson = options.errorsJson ? JSON.parse(options.errorsJson) : validation.errorsJson;
            validation.type = options.type ? options.type : validation.type;
        }

        return validation;
    },

    showErrors: function() {
        if (validation.type == validation.types.admin) {
            $.each(validation.errorsJson,
                function(index, error) {
                    var selector = '.Code-' + error.Code;

                    $(selector).addClass('has-error');
                    $(selector).find('.error-text').text(error.ErrorMessage);
                });
        }
    },

    hideErrors: function() {
        $('.has-error .error-text').text('');
        $('.has-error').removeClass('has-error');
    }
};