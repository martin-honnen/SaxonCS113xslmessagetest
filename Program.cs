using Saxon.Api;

var processor = new Processor();

var xsltCompiler = processor.NewXsltCompiler();
xsltCompiler.BaseUri = new Uri("urn:from-string");

var xsltExecutable = xsltCompiler.Compile(new StringReader(@"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='3.0' expand-text='yes' xmlns:saxon='http://saxon.sf.net/' exclude-result-prefixes='#all'>
  <xsl:output indent='yes'/>
  <xsl:template match='/' name='xsl:initial-template'>
    <test>Run with {system-property('xsl:product-version')} at {saxon:timestamp()}</test>
    <xsl:message>Message test at {saxon:timestamp()}</xsl:message>
  </xsl:template>
</xsl:stylesheet>"));

var transformer = xsltExecutable.Load30();

transformer.MessageListener = message => { Console.Error.WriteLine(message); };

transformer.CallTemplate(null, processor.NewSerializer(Console.Out));
