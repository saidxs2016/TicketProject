# TicketProject
  * appsetting.json dosyalarında kendi rabbitmq host bilgilerinizi giriniz
  * consul service discovery'i ayağa kaldırın
  * token elde etmek için:
      * username: admin
      * password: 123
# Proje Yapısı:
  * 3 tane Servis: (Identity Service, TicketService, PaymentService)
  * 1 tane ApiGateway: (Wev.ApiGateway)
  * 1 tane UI amaçlı Api (Web.API)
  * 1 tane Message Bus (Contracts'ları içeriyor.)
  * 1 tane Info Classlibraray (projeyi deploay etmek için yardmcı olabliecek tüm commands veya compose-file'leri içeriyor.)
# Projedeki her servisin mimari:
  * 3 katmandan oluşuyor
    * 1. katman API|UI
      2. katman Application(arayüzden gelen tüm requestleri veya diğer servislerden fırlatılan tüm event'leri handle edecek katman).
      3. katman 2 tane class library içeriyor:
         * Core class library: ortak kullanıabilecek tüm metot, extensions, function,... içermektedir.
         * DAL : Veri taban işlemleri halledecek class library
  * Bu Mimari hakkında daha fazla bilgi için altaki repoyu inceleyebilirsiniz:
      https://github.com/saidxs2016/BaseAdminUI
