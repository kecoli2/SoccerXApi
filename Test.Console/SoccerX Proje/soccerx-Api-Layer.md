# ⚽ SoccerX - Proje Yapısı (Clean Architecture)

## 📁 Solution: SoccerX.sln

```plaintext
📦 SoccerX
├── 📂 SoccerX.API                      # API Katmanı (Controller'lar & SignalR)
│   ├── 📂 Controllers                    # HTTP Controller'ları
│   ├── 📂 Hubs                           # SignalR Hub'ları
│   ├── Program.cs                       # Giriş noktası
│   ├── appsettings.json                 # Genel konfigürasyon dosyası
│
├── 📂 SoccerX.Application             # Application Katmanı (CQRS & İş Kuralları)
│   ├── 📂 Commands                      # MediatR Command işlemleri
│   ├── 📂 Queries                       # MediatR Query işlemleri
│   ├── 📂 Validators                    # FluentValidation sınıfları
│   ├── 📂 Interfaces                    # IRepository, ICacheService, IJobService vs.
│   ├── 📂 Services                      # Application-level iş mantığı
│
├── 📂 SoccerX.Domain                  # Domain Katmanı (Temel Modeller & Enumlar)
│   ├── 📂 Entities                      # Veritabanı Entity tanımları
│   ├── 📂 Enums                         # PostgreSQL enum type eşlemeleri
│
├── 📂 SoccerX.DTO                     # DTO Katmanı (Veri Transfer Nesneleri)
│   ├── 📂 Requests                      # API’ye gelen DTO'lar
│   ├── 📂 Responses                     # API’den dönen DTO'lar
│   ├── 📂 Mappers                       # DTO <-> Entity dönüşümleri
│
├── 📂 SoccerX.Infrastructure          # Dış Servis Entegrasyonları
│   ├── 📂 Caching                       # Redis cache servisi
│   ├── 📂 Jobs                          # Quartz zamanlanmış görevleri
│   ├── 📂 Notifications                 # Bildirim altyapısı (opsiyonel)
│
├── 📂 SoccerX.Persistence             # EF Core Katmanı
│   ├── 📂 Context                       # DbContext, Migration ayarları
|   |   ├──📂 Configuration              # Fluent API konfigurasyonları
│   ├── 📂 Repositories                  # Repository implementasyonları
│   ├── 📂 Interfaces                    # Entity’lere özel repository interface’leri
│
├── 📂 SoccerX.Common                  # Ortak Yardımcı Araçlar
│   ├── 📂 Extensions                    # Extension method'lar
│   ├── 📂 Configuration                 # Redis, Quartz, Database vs. config class'ları
│   ├── 📂 Security                      # Encryption/Decryption araçları
│   ├── 📂 Helpers                       # Küçük yardımcı sınıflar
│
├── 📂 SoccerX.Tests                   # Test Katmanı
│   ├── 📂 UnitTests                     # Unit test senaryoları
│   ├── 📂 IntegrationTests              # Gerçek veritabanı ile testler
│
├── SoccerX.sln                        # Çözüm dosyası

🔄 Katmanlar Arası Bağımlılık (Dependency Yönü)
        Proje	Bağımlı Olduğu Projeler
        SoccerX.API	Application, DTO, Common, SignalR
        Application	Domain, DTO, Common
        Infrastructure	Application, Common
        Persistence	Domain, Application, Common
        Tests	API, Application, Domain, Common
📌 Notlar:
        Entity'ler sadece Domain içinde tutulur.
        Fluent API konfigürasyonları Persistence/Configuration içindedir.
        Tüm dış sistemler (Redis, Quartz, vs.) Infrastructure içinde konumlandırılır.
        Ortak kullanılan sınıflar (helpers, config) Common klasöründedir.


