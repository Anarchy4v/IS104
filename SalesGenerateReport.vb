
Public Class SalesGenerateReport
    Private Sub SalesGenerateReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim currentDateAndTime As DateTime = DateTime.Now
        Label2.Text = $"{currentDateAndTime}"

        Label4.Text = SharedSales.SalesAmount.ToString("C", New System.Globalization.CultureInfo("en-PH"))
        Label6.Text = SharedSales.SalesTax.ToString("C", New System.Globalization.CultureInfo("en-PH"))
        Label8.Text = SharedSales.SalesTotal.ToString("C", New System.Globalization.CultureInfo("en-PH"))
    End Sub
End Class