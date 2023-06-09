Imports Newtonsoft.Json
Public Class ProductsResponse
    'https://www.tutlane.com/tutorial/visual-basic/vb-inheritance
    Inherits BaseResponse
    <JsonProperty(PropertyName:="products", NullValueHandling:=NullValueHandling.Ignore)>
    Property Products As Product()

End Class
