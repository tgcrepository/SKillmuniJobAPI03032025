// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CouponsRedeemed
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class CouponsRedeemed
  {
    public int id_CouponsRedeemed { get; set; }

    public string CouponID { get; set; }

    public string WebsiteName { get; set; }

    public string CouponCode { get; set; }

    public string CouponDescription { get; set; }

    public string Link { get; set; }

    public string PointsUsed { get; set; }

    public string Image { get; set; }

    public string ExpiryDate { get; set; }

    public int usedmoney { get; set; }
  }
}
