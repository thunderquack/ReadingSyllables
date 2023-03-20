namespace ReadingSyllables.Services
{
    internal class TitleService
    {
#pragma warning disable CS8618
        private FormSyllables requiredForm;
#pragma warning restore CS8618

        internal void SetRequiredForm(FormSyllables requiredForm)
        {
            this.requiredForm = requiredForm;
        }

        internal async Task SetTitle(string title)
        {
            await Task.Run(() =>
            {
                string temp = requiredForm.Text;
                if (requiredForm.InvokeRequired)
                {
                    requiredForm.Invoke((MethodInvoker)delegate { requiredForm.SetTitle(title); });
                }
                requiredForm.Text = title;
                Thread.Sleep(2000);
                if (requiredForm.Text == title)
                {
                    if (requiredForm.InvokeRequired)
                    {
                        requiredForm.Invoke((MethodInvoker)delegate { requiredForm.SetTitle(temp); });
                    }
                }
            });
        }
    }
}