export class FormUtils {

  public static CheckSubmitting() {
    $('form').on('submit', function (e) {
      
      if (typeof (grecaptcha) !== undefined && grecaptcha.getResponse() === '') {
        e.preventDefault();
        return;
      }
      
      FormUtils.PreventDoubleSubmit($(this))
    });
  }

  private static PreventDoubleSubmit(form: JQuery<HTMLElement>) {
    if (form.valid()) {
      let btn = form.find(':submit');
      btn.prop("disabled", true);
      btn.text('Processing...');
      btn.prepend('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
    }
  }
}
