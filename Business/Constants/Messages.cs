using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    //Constants=sabit
    //Sürekli new'lememek için static yapılır.sabit
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductListed = "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "bir kategoride en fazla 10 ürün bulunabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategoride limit aşıldıgı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string UserRegistered = "Kayıt oldu";
    }
}
