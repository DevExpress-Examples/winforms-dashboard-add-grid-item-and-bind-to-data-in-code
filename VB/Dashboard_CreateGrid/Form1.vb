Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraEditors

Namespace Dashboard_CreateGrid
	Partial Public Class Form1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

			' Creates the Excel data source.
			Dim excelDataSource As New DashboardExcelDataSource()
			excelDataSource = CreateExcelDataSource()

			' Creates the Grid dashboard item and adds it to a dashboard.
			dashboardViewer1.Dashboard = New Dashboard()
			dashboardViewer1.Dashboard.DataSources.Add(excelDataSource)
			Dim grid As GridDashboardItem = CreateGrid(excelDataSource)
			dashboardViewer1.Dashboard.Items.Add(grid)

			' Reloads data in the data sources.
			dashboardViewer1.ReloadData()
		End Sub
		Private Function CreateGrid(ByVal dataSource As DashboardExcelDataSource) As GridDashboardItem

			' Creates a grid dashboard item and specifies its data source.
			Dim grid As New GridDashboardItem()
			grid.DataSource = dataSource

			' Creates new grid columns of the specified type and with the specified dimension or
			' measure. Then, adds these columns to the grid's Columns collection.
			grid.Columns.Add(New GridHyperlinkColumn(New Dimension("Product"), "Product") With {.UriPattern= "https://www.google.com/search?q={0}"})
			grid.Columns.Add(New GridDimensionColumn(New Dimension("Category")))
			grid.Columns.Add(New GridMeasureColumn(New Measure("Count")))
			grid.Columns.Add(New GridDeltaColumn(New Measure("Count"), New Measure("TargetCount")))
			grid.Columns.Add(New GridSparklineColumn(New Measure("Count")))
			grid.SparklineArgument = New Dimension("Date", DateTimeGroupInterval.MonthYear)

			grid.GridOptions.EnableBandedRows = True
			grid.GridOptions.ShowHorizontalLines = False

			Return grid
		End Function

		Public Function CreateExcelDataSource() As DashboardExcelDataSource

			' Generates the Excel Data Source.
			Dim excelDataSource As New DashboardExcelDataSource()
			excelDataSource.FileName = "Data\SimpleDataSource.xls"
			Dim worksheetSettings As New ExcelWorksheetSettings("Simple Data", "A1:F12")
			excelDataSource.SourceOptions = New ExcelSourceOptions(worksheetSettings)
			excelDataSource.Fill()

			Return excelDataSource
		End Function
	End Class
End Namespace
