/// <reference path="../../node_modules/@types/jquery/index.d.ts" />
/// <reference path="../../node_modules/@types/jquery.validation/index.d.ts" />
/// <reference path="../../node_modules/@types/grecaptcha/index.d.ts" />

import { FormUtils } from './form.utils';

class Main {

  constructor() {
    $(function () {
      FormUtils.CheckSubmitting();
    });
  }
}

var main: Main = new Main();
