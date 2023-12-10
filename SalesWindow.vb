Public Class SalesWindow
    Private orderDataTable As DataTable = CreateOrderDataTable()
    Public Shared salesWindowInstance As SalesWindow

    Public Class OrderDetails
        Public Property ItemName As String
        Public Property Quantity As Integer
        Public Property Price As Decimal
    End Class

    Public Sub AddOrderToDataTable(itemName As String, quantity As Integer, price As Decimal)
        orderDataTable.Rows.Add(itemName, quantity, price)

        ' Refresh the DataGridView
        DataGridView1.DataSource = Nothing
        DataGridView1.DataSource = orderDataTable
    End Sub

    Private Sub LoadModalOrderForm()
        'provide a way to load the data that has submitted during Add Order (Button7
    End Sub

    Private Sub SalesWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the data source
        DataGridView1.DataSource = orderDataTable
        LoadModalOrderForm()
    End Sub

    Private Function CreateOrderDataTable() As DataTable
        Dim dataTable As New DataTable()
        With dataTable.Columns
            .Add("ItemName", GetType(String))
            .Add("Quantity", GetType(Integer))
            .Add("Price", GetType(Decimal))
        End With

        Return dataTable
    End Function
End Class
