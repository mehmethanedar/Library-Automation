# Library-Automation

İŞ KURALLARI
- Bir kullanıcıya bir iletişim bilgisi aittir. Bir iletişim bilgisi bir kullanıcıya aittir.
- Bir kullanıcı hiç ya da birden çok kitap ödünç alabilir. Bir kitap hiç ya da birden çok
kullanıcı tarafından ödünç alınabilir.
- Bir kullanıcı hiç ya da birden çok bilgisayar ödünç alabilir. Bir bilgisayar hiç ya da
birden çok kullanıcı tarafından ödünç alınabilir.
- Bir iletişim bilgisine bir il aittir. Bir il birden çok iletişim bilgisine ait olabilir.
- Bir iletişim bilgisine bir ilçe aittir. Bir ilçe birden çok iletişim bilgisine ait olabilir.
- Bir iletişim bilgisine bir okul aittir. Bir okul birden çok iletişim bilgisine ait olabilir.
- Bir kitaba sadece bir kategori aittir. Bir kategori hiç ya da birden çok kitaba ait olabilir.


İlişkisel Şema
- Kullanici (kullaniciID: integer, kullaniciAdi: String, sifre: String, tcNo: String,
adSoyad: integer, cinsiyet: String, doğumTarihi: Date, KullaniciTur: String)
- Okul (OkulID:integer, okulAdi: String, ilceID:integer)
- Personel(kullaniciID:integer,yetki:String)
- Uye(kullaniciID:integer, kayitTarihi:date, okuduğuKitapSayisi:integer)
- KitapKayit(kitapKayitID:integer, kullaniciID:integer, kitapID:integer, alisTarihi:date,
verisTarihi:date)
- Kitap(kitapID:integer, isbn:integer, kitapAdi:String, yayinEviAdi:String,
yazarAdi:String, stokSayisi:integer, basimTarihi:date, ciltNo:integer,
kategoriID:integer)
- Kategori(KategoriID:integer, kategoriAdi:String)
- IletisimBilgileri(iletisimID:integer, telefon:integer, adres:String, ePosta:String,
ilID:integer, ilceID:integer, okulID:integer)
- Ilce(ilceID:integer, ilceAdi:String, ilID:integer)
- Il(ilID:integer, ilAdi:String)
- BilgisayarKayit (bilgisayarKayitID:integer, bilgisayarID: integer, kullaniciID:integer,
alisTarihi:date, verisTarihi:date)
- Bilgisayar(bilgisayarID:integer, marka:String, model:String, stokSayisi:integer)
