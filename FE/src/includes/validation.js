import {
  Form as VeeForm, Field as VeeField, defineRule, ErrorMessage, configure,
} from 'vee-validate';
import {
  required, min, max, alpha_spaces as alphaSpaces, email,
  min_value as minVal, max_value as maxVal, confirmed, not_one_of as excluded,
} from '@vee-validate/rules';

export default {
  install(app) {
    app.component('VeeForm', VeeForm);
    app.component('VeeField', VeeField);
    app.component('ErrorMessage', ErrorMessage);
    const isValidPhone= (value) => {
      const phoneRegex = /^\+?[0-9]{10,15}$/;
      return phoneRegex.test(value);
    };
    defineRule('required', required);
    defineRule('tos', required);
    defineRule('min', min);
    defineRule('max', max);
    defineRule('alpha_spaces', alphaSpaces);
    defineRule('email', email);
    defineRule('min_value', minVal);
    defineRule('max_value', maxVal);
    defineRule('passwords_mismatch', confirmed);
    defineRule('excluded', excluded);
    defineRule('phone',isValidPhone)
    configure({
      generateMessage: (ctx) => {
        const filterString = ctx.field.replace(/([A-Z])/g, " $1")
        const field = filterString.charAt(0).toUpperCase() + filterString.slice(1)
        const messages = {
          required: `The field ${field} is required.`,
          min: `The field ${field} is too short.`,
          max: `The field ${field} is too long.`,
          alpha_spaces: `The field ${field} may only contain alphabetical characters and spaces.`,
          email: `The field ${field} must be a valid email.`,
          phone: `The field ${field} must be a valid phone number`,
          min_value: `The field ${field} is too low.`,
          max_value: `The field ${field} is too high.`,
          excluded: `You are not allowed to use this value for the field ${field}.`,
          passwords_mismatch: "The passwords don't match.",
          tos: 'You must accept the Terms of Service.',
        };

        const message = messages[ctx.rule.name]
          ? messages[ctx.rule.name]
          : `The field ${field} is invalid.`;

        return message;
      },
      validateOnBlur: true,
      validateOnChange: true,
      validateOnInput: true,
      validateOnModelUpdate: true,
    });
  },
};
