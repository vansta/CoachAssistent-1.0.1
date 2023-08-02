import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import '@mdi/font/css/materialdesignicons.css';

export default createVuetify(
  {
    // theme: {
    //     defaultTheme: 'myCustomLightTheme',
    //     themes: {
    //         myCustomLightTheme
    //     }
    // },
    defaults: {
      VTextField: {
        variant: 'solo',
        density: 'compact'
      },
      VTextarea: {
        variant: 'solo',
        density: 'compact'
      },
      VSelect: {
        variant: 'solo',
        density: 'compact'
      },
      VAutocomplete: {
        variant: 'solo',
        density: 'compact'
      }
    }
}
);