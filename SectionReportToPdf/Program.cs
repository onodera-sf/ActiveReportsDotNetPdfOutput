// レポートファイルがあるパス
var reportPath = "SectionReportToPdf.Reports.Invoice.rpx";

// 埋め込みリソースを Stream をして取り出す
var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(reportPath);
if (stream == null) return;

// Stream を XML として読み込めるようにします
var xr = System.Xml.XmlReader.Create(stream);

// セクションレポートとして読み込みます
var sectionReport = new GrapeCity.ActiveReports.SectionReport();
sectionReport.LoadLayout(xr);
sectionReport.Run();

// PDF のエクスポートフィルターを作成します
var pdfExport = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();

// メモリストリームに PDF の情報を書き出します
var outputStream = new System.IO.MemoryStream();
pdfExport.Export(sectionReport.Document, outputStream);
outputStream.Seek(0, SeekOrigin.Begin);

// ファイルにストリームを書き出します
using var fs = new FileStream("Output.pdf", FileMode.Create);
outputStream.CopyTo(fs);
