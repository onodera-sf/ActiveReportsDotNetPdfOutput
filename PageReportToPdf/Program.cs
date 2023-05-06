// レポートファイルがあるパス
var reportPath = "PageReportToPdf.Reports.Estimate_page_ipa.rdlx";

// 埋め込みリソースを Stream をして取り出す
var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(reportPath);
if (stream == null) return;

// ページレポートに変換します
using var sr = new StreamReader(stream);
var pageReport = new GrapeCity.ActiveReports.PageReport(sr);

// PDF 描画拡張を定義します
var pdfRenderingExtension = new GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension();

// メモリストリームとして出力する定義です
var outputProvider = new GrapeCity.ActiveReports.Rendering.IO.MemoryStreamProvider();

// レポートを PDF にレンダリングします
pageReport.Document.Render(pdfRenderingExtension, outputProvider);

// Stream に変換します
var ps = outputProvider.GetPrimaryStream();
var outputStream = ps.OpenStream();

// ファイルにストリームを書き出します
using var fs = new FileStream("Output.pdf", FileMode.Create);
outputStream.CopyTo(fs);
