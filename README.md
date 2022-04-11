# STOK BILDIRIM REST API

### Bu Api, bir microservice mimarisinin parcasidir.
Web veya mobile uzerinden bir urunun stokta olmadigi durumda, kayitli kullanicinin "gelince haber ver" secenegini ile urun stok takibi yapabilmesini 
saglamayi amaclamaktadir.

Bildirimleri depolar ve onlari yonetir. Tetiklendiginde ilgili servislere (Web Notification ve Mobile Push Notification) bildirim bilgisini 
(ProductId, UserId) gonderir.

*Kullanici ister webden isterse mobileden bildirim talep etmis olsun, iki sekildede bildirim saglanir. Mobile uygulamayi yuklememis kullanicilara 
gonderilecek bildirimin takibini Mobile Push Notification hizmeti yapacaktir. Bu hizmetin amaci sadece bildirimleri yonetmektir.
