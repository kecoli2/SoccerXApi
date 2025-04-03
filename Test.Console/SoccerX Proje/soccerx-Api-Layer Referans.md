SoccerX.API
 ├── references: SoccerX.Application, SoccerX.DTO
 
SoccerX.Application
 ├── references: SoccerX.Domain
 ├── references: SoccerX.DTO
 ├── references: SoccerX.Common

SoccerX.Infrastructure
 ├── references: SoccerX.Application
 ├── references: SoccerX.Domain
 ├── references: SoccerX.Common

SoccerX.Persistence
 ├── references: SoccerX.Application
 ├── references: SoccerX.Domain
 ├── references: SoccerX.Common

SoccerX.DTO
 ├── references: SoccerX.Domain (isteğe bağlı - sadece entity isimleri için)
 └── 🔸 Genellikle bağımsız olmalı

SoccerX.Domain
 └── references: (hiçbir katmanı referans almaz — bağımsız ve saf katmandır)

SoccerX.Common
 └── references: (hiçbir katmanı referans almaz — yardımcı sınıflar, config modelleri, extensionlar içerir)

SoccerX.Tests
 ├── references: SoccerX.API, SoccerX.Application, SoccerX.Persistence, SoccerX.Infrastructure, SoccerX.Common, SoccerX.Domain
