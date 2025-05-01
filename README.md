# Randevu Sistemi

Bu proje, kullanıcıların randevu oluşturup yönetebileceği ve yöneticilerin tüm randevuları görebileceği bir web uygulamasıdır. Kullanıcılar kendi randevularını oluşturabilir, düzenleyebilir ve silebilirken, yöneticiler tüm randevuları görüntüleyebilir ve yönetebilir.

![Randevu Sistemi](/screenshots/randevu-sistemi.png)

## 🚀 Özellikler

- Kullanıcı kaydı ve girişi
- Güvenli şifre hashleme (PBKDF2)
- Randevu oluşturma, düzenleme ve silme
- Yönetici paneli ile tüm randevuları görüntüleme
- Silinmiş randevuları geri yükleme
- E-posta bildirimleri (mock)

## 💻 Kullanılan Teknolojiler

- **Backend**: ASP.NET Core 9.0 MVC
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript
- **Veritabanı**: SQLite
- **ORM**: Entity Framework Core
- **Mimari**: N-Katmanlı Mimari (Core, Data, Service, Web)
- **Güvenlik**: ASP.NET Core Identity, PBKDF2 şifre hashleme
- **Diğer**: Dependency Injection, Repository Pattern

## 🔧 Projeyi Çalıştırmak İçin Gerekli Adımlar

### Gereksinimler

- .NET 9.0 SDK veya üzeri
- Visual Studio 2022 (önerilen) veya herhangi bir kod editörü

### Adımlar

1. **Kaynak kodunu klonlayın**
   ```bash
   git clone https://github.com/kullaniciadi/RandevuSistemi.git
   cd RandevuSistemi
   ```

2. **Projeyi yapılandırın**
   ```bash
   dotnet restore
   ```

3. **Veritabanını oluşturun**
   ```bash
   cd RandevuSistemi
   dotnet ef database update
   ```
   
   *Not: Eğer Entity Framework CLI yüklü değilse:*
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Uygulamayı çalıştırın**
   ```bash
   dotnet run
   ```

5. **Tarayıcınızı açın**
   - Uygulama varsayılan olarak `http://localhost:44304` adresinde çalışacaktır.

### Varsayılan Kullanıcılar

Uygulama ilk kez çalıştırıldığında, aşağıdaki varsayılan kullanıcılar oluşturulur:

- **Yönetici**
  - Kullanıcı adı: `admin`
  - Şifre: `admin123`
  
- **Normal Kullanıcı**
  - Kullanıcı adı: `user`
  - Şifre: `user123`

## 🏗️ Proje Yapısı

```
RandevuSistemi/
├── RandevuSistemi.Core/          # Varlıklar, arayüzler, sabitler
├── RandevuSistemi.Data/          # Repository implementasyonları, veritabanı erişimi
├── RandevuSistemi.Service/       # İş mantığı servisleri
└── RandevuSistemi/               # Web uygulaması (MVC)
    ├── Controllers/
    ├── Models/
    ├── Views/
    └── wwwroot/                  # Statik dosyalar (CSS, JS)
```

## 🤔 Geliştirme Sırasında Yapılan Varsayımlar ve Yaklaşımlar

### Mimari Yaklaşım

- **N-Katmanlı Mimari**: Uygulama, farklı sorumlulukları ayırmak için N-katmanlı mimari kullanılarak geliştirildi. Core, Data, Service ve Web katmanları, kodun daha yönetilebilir ve test edilebilir olmasını sağlar.

- **Repository Pattern**: Veritabanı işlemleri için repository pattern kullanıldı. Bu, veri erişim mantığını servis katmanından ayırarak, kodun daha modüler ve test edilebilir olmasını sağlar.

- **Dependency Injection**: Tüm servisler ve repository'ler, dependency injection kullanılarak bağımlılıkları yönetir.

### Güvenlik Yaklaşımları

- **Şifre Hashleme**: Kullanıcı şifreleri, PBKDF2 algoritması kullanılarak güvenli bir şekilde hashlenmiştir.

- **Yetkilendirme**: Yönetici ve kullanıcı rollerinin farklı yetkilere sahip olması sağlanmıştır.

### Kullanıcı Deneyimi

- **Randevu Durumları**: Randevular, "Aktif", "Geçmiş" ve "İptal Edilmiş" olarak işaretlenir ve görsel olarak farklılaştırılır.

- **E-posta Bildirimleri**: Kullanıcılar, randevu işlemleri hakkında e-posta ile bilgilendirilir.

### Varsayımlar

- **Silme İşlemi**: Randevular gerçekten silinmek yerine "soft delete" yaklaşımıyla işaretlenir.

- **Çakışma Kontrolü**: Aynı saatte birden fazla randevu oluşturulabilir.

- **Tarih/Saat**: Sistem, randevuların geçmiş tarih ve saatlere oluşturulmasını engeller.

## 📝 Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.

## 🙏 Teşekkürler

Bu proje, ASP.NET Core ve Entity Framework Core'un sağladığı güçlü altyapı sayesinde geliştirilebilmiştir.
