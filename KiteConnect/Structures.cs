﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiteConnect
{
    /// <summary>
    /// Tick data structure
    /// </summary>
    public struct Tick
    {
        public string Mode { get; set; }
        public UInt32 InstrumentToken { get; set; }
        public bool Tradable { get; set; }
        public decimal LastPrice { get; set; }
        public UInt32 LastQuantity { get; set; }
        public decimal AveragePrice { get; set; }
        public UInt32 Volume { get; set; }
        public UInt32 BuyQuantity { get; set; }
        public UInt32 SellQuantity { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Change { get; set; }
        public DepthItem[] Bids { get; set; }
        public DepthItem[] Offers { get; set; }

        // KiteConnect 3 Fields

        public DateTime? LastTradeTime { get; set; }
        public UInt32 OpenInterest { get; set; }
        public UInt32 DayHighOpenInterest { get; set; }
        public UInt32 DayLowOpenInterest { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Market depth item structure
    /// </summary>
    public struct DepthItem
    {
        public DepthItem(Dictionary<string, dynamic> data)
        {
            Quantity = Convert.ToUInt32(data["quantity"]);
            Price = data["price"];
            Orders = Convert.ToUInt32(data["orders"]);
        }

        public UInt32 Quantity { get; set; }
        public decimal Price { get; set; }
        public UInt32 Orders { get; set; }
    }

    /// <summary>
    /// Historical structure
    /// </summary>
    public struct Historical
    {
        public Historical(ArrayList data)
        {
            TimeStamp = Convert.ToString(data[0]);
            Open = Convert.ToDecimal(data[1]);
            High = Convert.ToDecimal(data[2]);
            Low = Convert.ToDecimal(data[3]);
            Close = Convert.ToDecimal(data[4]);
            Volume = Convert.ToUInt32(data[5]);
        }

        public string TimeStamp { get; }
        public decimal Open { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public decimal Close { get; }
        public UInt32 Volume { get; }
    }

    /// <summary>
    /// Holding structure
    /// </summary>
    public struct Holding
    {
        public Holding(Dictionary<string, dynamic> data)
        {
            try
            {
                Product = data["product"];
                Exchange = data["exchange"];
                Price = data["price"];
                LastPrice = data["last_price"];
                CollateralQuantity = data["collateral_quantity"];
                PNL = data["pnl"];
                ClosePrice = data["close_price"];
                AveragePrice = data["average_price"];
                TradingSymbol = data["tradingsymbol"];
                CollateralType = data["collateral_type"];
                T1Quantity = data["t1_quantity"];
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                ISIN = data["isin"];
                RealisedQuantity = data["realised_quantity"];
                Quantity = data["quantity"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }

        public string Product { get; set; }
        public string Exchange { get; set; }
        public decimal Price { get; set; }
        public decimal LastPrice { get; set; }
        public int CollateralQuantity { get; set; }
        public decimal PNL { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal AveragePrice { get; set; }
        public string TradingSymbol { get; set; }
        public string CollateralType { get; set; }
        public int T1Quantity { get; set; }
        public UInt32 InstrumentToken { get; set; }
        public string ISIN { get; set; }
        public int RealisedQuantity { get; set; }
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Available margin structure
    /// </summary>
    public struct AvailableMargin
    {
        public AvailableMargin(Dictionary<string, dynamic> data)
        {
            try
            {
                AdHocMargin = data["adhoc_margin"];
                Cash = data["cash"];
                Collateral = data["collateral"];
                IntradayPayin = data["intraday_payin"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }

        public decimal AdHocMargin { get; set; }
        public decimal Cash { get; set; }
        public decimal Collateral { get; set; }
        public decimal IntradayPayin { get; set; }
    }

    /// <summary>
    /// Utilised margin structure
    /// </summary>
    public struct UtilisedMargin
    {
        public UtilisedMargin(Dictionary<string, dynamic> data)
        {
            try
            {
                Debits = data["debits"];
                Exposure = data["exposure"];
                M2MRealised = data["m2m_realised"];
                M2MUnrealised = data["m2m_unrealised"];
                OptionPremium = data["option_premium"];
                Payout = data["payout"];
                Span = data["span"];
                HoldingSales = data["holding_sales"];
                Turnover = data["turnover"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }

        public decimal Debits { get; set; }
        public decimal Exposure { get; set; }
        public decimal M2MRealised { get; set; }
        public decimal M2MUnrealised { get; set; }
        public decimal OptionPremium { get; set; }
        public decimal Payout { get; set; }
        public decimal Span { get; set; }
        public decimal HoldingSales { get; set; }
        public decimal Turnover { get; set; }

    }

    /// <summary>
    /// UserMargin structure
    /// </summary>
    public struct UserMargin
    {
        public UserMargin(Dictionary<string, dynamic> data)
        {
            try
            {
                Enabled = data["enabled"];
                Net = data["net"];
                Available = new AvailableMargin(data["available"]);
                Utilised = new UtilisedMargin(data["utilised"]);
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }

        public bool Enabled { get; set; }
        public decimal Net { get; set; }
        public AvailableMargin Available { get; set; }
        public UtilisedMargin Utilised { get; set; }
    }

    /// <summary>
    /// User margins response structure
    /// </summary>
    public struct UserMarginsResponse
    {
        public UserMarginsResponse(Dictionary<string, dynamic> data)
        {
            try
            {
                Equity = new UserMargin(data["equity"]);
                Commodity = new UserMargin(data["commodity"]);
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }
        public UserMargin Equity { get; set; }
        public UserMargin Commodity { get; set; }
    }

    /// <summary>
    /// UserMargin structure
    /// </summary>
    public struct InstrumentMargin
    {
        public InstrumentMargin(Dictionary<string, dynamic> data)
        {
            try
            {
                Margin = data["margin"];
                COLower = data["co_lower"];
                MISMultiplier = data["mis_multiplier"];
                Tradingsymbol = data["tradingsymbol"];
                COUpper = data["co_upper"];
                NRMLMargin = data["nrml_margin"];
                MISMargin = data["mis_margin"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }
        }

        public string Tradingsymbol { get; set; }
        public decimal Margin { get; set; }
        public decimal COLower { get; set; }
        public decimal COUpper { get; set; }
        public decimal MISMultiplier { get; set; }
        public decimal MISMargin { get; set; }
        public decimal NRMLMargin { get; set; }
    }
    /// <summary>
    /// Position structure
    /// </summary>
    public struct Position
    {
        public Position(Dictionary<string, dynamic> data)
        {
            try
            {
                Product = data["product"];
                OvernightQuantity = data["overnight_quantity"];
                Exchange = data["exchange"];
                SellValue = data["sell_value"];
                BuyM2MValue = data["buy_m2m_value"];
                LastPrice = data["last_price"];
                TradingSymbol = data["tradingsymbol"];
                Realised = data["realised"];
                PNL = data["pnl"];
                Multiplier = data["multiplier"];
                SellQuantity = data["sell_quantity"];
                SellM2MValue = data["sell_m2m_value"];
                BuyValue = data["buy_value"];
                BuyQuantity = data["buy_quantity"];
                AveragePrice = data["average_price"];
                Unrealised = data["unrealised"];
                Value = data["value"];
                BuyPrice = data["buy_price"];
                SellPrice = data["sell_price"];
                M2M = data["m2m"];
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                ClosePrice = data["close_price"];
                Quantity = data["quantity"];
                DayBuyQuantity = data["day_buy_quantity"];
                DayBuyValue = data["day_buy_value"];
                DayBuyPrice = data["day_buy_price"];
                DaySellQuantity = data["day_sell_quantity"];
                DaySellValue = data["day_sell_value"];
                DaySellPrice = data["day_sell_price"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string Product { get; }
        public int OvernightQuantity { get; }
        public string Exchange { get; }
        public decimal SellValue { get; }
        public decimal BuyM2MValue { get; }
        public decimal LastPrice { get; }
        public string TradingSymbol { get; }
        public decimal Realised { get; }
        public decimal PNL { get; }
        public decimal Multiplier { get; }
        public int SellQuantity { get; }
        public decimal SellM2MValue { get; }
        public decimal BuyValue { get; }
        public int BuyQuantity { get; }
        public decimal AveragePrice { get; }
        public decimal Unrealised { get; }
        public decimal Value { get; }
        public decimal BuyPrice { get; }
        public decimal SellPrice { get; }
        public decimal M2M { get; }
        public UInt32 InstrumentToken { get; }
        public decimal ClosePrice { get; }
        public int Quantity { get; }
        public int DayBuyQuantity { get; }
        public decimal DayBuyPrice { get; }
        public decimal DayBuyValue { get; }
        public int DaySellQuantity { get; }
        public decimal DaySellPrice { get; }
        public decimal DaySellValue { get; }
    }

    /// <summary>
    /// Position response structure
    /// </summary>
    public struct PositionResponse
    {
        public PositionResponse(Dictionary<string, dynamic> data)
        {
            Day = new List<Position>();
            Net = new List<Position>();

            foreach (Dictionary<string, dynamic> item in data["day"])
                Day.Add(new Position(item));
            foreach (Dictionary<string, dynamic> item in data["net"])
                Net.Add(new Position(item));
        }

        public List<Position> Day { get; }
        public List<Position> Net { get; }
    }

    /// <summary>
    /// Order structure
    /// </summary>
    public struct Order
    {
        public Order(Dictionary<string, dynamic> data)
        {
            try
            {
                AveragePrice = data["average_price"];
                CancelledQuantity = data["cancelled_quantity"];
                DisclosedQuantity = data["disclosed_quantity"];
                Exchange = data["exchange"];
                ExchangeOrderId = data["exchange_order_id"];
                ExchangeTimestamp = data["exchange_timestamp"];
                FilledQuantity = data["filled_quantity"];
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                MarketProtection = data["market_protection"];
                OrderId = data["order_id"];
                OrderTimestamp = data["order_timestamp"];
                OrderType = data["order_type"];
                ParentOrderId = data["parent_order_id"];
                PendingQuantity = data["pending_quantity"];
                PlacedBy = data["placed_by"];
                Price = data["price"];
                Product = data["product"];
                Quantity = data["quantity"];
                Status = data["status"];
                StatusMessage = data["status_message"];
                Tag = data["tag"];
                Tradingsymbol = data["tradingsymbol"];
                TransactionType = data["transaction_type"];
                TriggerPrice = data["trigger_price"];
                Validity = data["validity"];
                Variety = data["variety"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public decimal AveragePrice { get; set; }
        public int CancelledQuantity { get; set; }
        public int DisclosedQuantity { get; set; }
        public string Exchange { get; set; }
        public string ExchangeOrderId { get; set; }
        public string ExchangeTimestamp { get; set; }
        public int FilledQuantity { get; set; }
        public UInt32 InstrumentToken { get; set; }
        public int MarketProtection { get; set; }
        public string OrderId { get; set; }
        public string OrderTimestamp { get; set; }
        public string OrderType { get; set; }
        public string ParentOrderId { get; set; }
        public int PendingQuantity { get; set; }
        public string PlacedBy { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string Tag { get; set; }
        public string Tradingsymbol { get; set; }
        public string TransactionType { get; set; }
        public decimal TriggerPrice { get; set; }
        public string Validity { get; set; }
        public string Variety { get; set; }
    }

    /// <summary>
    /// Order Info structure
    /// </summary>
    public struct OrderInfo
    {
        public OrderInfo(Dictionary<string, dynamic> data)
        {
            try
            {
                AveragePrice = data["average_price"];
                DisclosedQuantity = data["disclosed_quantity"];
                Exchange = data["exchange"];
                ExchangeOrderId = data["exchange_order_id"];
                OrderId = data["order_id"];
                OrderTimestamp = data["order_timestamp"];
                OrderType = data["order_type"];
                PendingQuantity = data["pending_quantity"];
                Price = data["price"];
                Product = data["product"];
                Quantity = data["quantity"];
                Status = data["status"];
                StatusMessage = data["status_message"];
                TransactionType = data["transaction_type"];
                TriggerPrice = data["trigger_price"];
                Validity = data["validity"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public decimal AveragePrice { get; set; }
        public int DisclosedQuantity { get; set; }
        public string Exchange { get; set; }
        public string ExchangeOrderId { get; set; }
        public string OrderId { get; set; }
        public string OrderTimestamp { get; set; }
        public string OrderType { get; set; }
        public int PendingQuantity { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string TransactionType { get; set; }
        public decimal TriggerPrice { get; set; }
        public string Validity { get; set; }
    }

    /// <summary>
    /// Instrument structure
    /// </summary>
    public struct Instrument
    {
        public Instrument(Dictionary<string, dynamic> data)
        {
            try
            {
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                ExchangeToken = Convert.ToUInt32(data["exchange_token"]);
                TradingSymbol = data["tradingsymbol"];
                Name = data["name"];
                LastPrice = Convert.ToDecimal(data["last_price"]);
                TickSize = Convert.ToDecimal(data["tick_size"]);
                Expiry = data["expiry"];
                InstrumentType = data["instrument_type"];
                Segment = data["segment"];
                Exchange = data["exchange"];

                if (data["strike"].Contains("e"))
                    Strike = Decimal.Parse(data["strike"], System.Globalization.NumberStyles.Float);
                else
                    Strike = Convert.ToDecimal(data["strike"]);

                LotSize = Convert.ToUInt32(data["lot_size"]);
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public UInt32 InstrumentToken { get; set; }
        public UInt32 ExchangeToken { get; set; }
        public string TradingSymbol { get; set; }
        public string Name { get; set; }
        public decimal LastPrice { get; set; }
        public decimal TickSize { get; set; }
        public string Expiry { get; set; }
        public string InstrumentType { get; set; }
        public string Segment { get; set; }
        public string Exchange { get; set; }
        public decimal Strike { get; set; }
        public UInt32 LotSize { get; set; }
    }

    /// <summary>
    /// Trade structure
    /// </summary>
    public struct Trade
    {
        public Trade(Dictionary<string, dynamic> data)
        {
            try
            {
                TradeId = data["trade_id"];
                OrderId = data["order_id"];
                ExchangeOrderId = data["exchange_order_id"];
                Tradingsymbol = data["tradingsymbol"];
                Exchange = data["exchange"];
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                TransactionType = data["transaction_type"];
                Product = data["product"];
                AveragePrice = data["average_price"];
                Quantity = data["quantity"];
                OrderTimestamp = data["order_timestamp"];
                ExchangeTimestamp = data["exchange_timestamp"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string TradeId { get; }
        public string OrderId { get; }
        public string ExchangeOrderId { get; }
        public string Tradingsymbol { get; }
        public string Exchange { get; }
        public UInt32 InstrumentToken { get; }
        public string TransactionType { get; }
        public string Product { get; }
        public decimal AveragePrice { get; }
        public int Quantity { get; }
        public string OrderTimestamp { get; }
        public string ExchangeTimestamp { get; }
    }

    /// <summary>
    /// Trigger range structure
    /// </summary>
    public struct TrigerRange
    {
        public TrigerRange(Dictionary<string, dynamic> data)
        {
            try
            {
                Start = data["start"];
                End = data["end"];
                Percent = data["percent"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }
        public decimal Start { get; }
        public decimal End { get; }
        public decimal Percent { get; }
    }

    /// <summary>
    /// User structure
    /// </summary>
    public struct User
    {
        public User(Dictionary<string, dynamic> data)
        {
            try
            {
                APIKey = data["data"]["api_key"];
                Products = (string[])data["data"]["products"].ToArray(typeof(string));
                UserName = data["data"]["user_name"];
                Broker = data["data"]["broker"];
                AccessToken = data["data"]["access_token"];
                PublicToken = data["data"]["public_token"];
                RefreshToken = data["data"]["refresh_token"];
                UserType = data["data"]["user_type"];
                UserId = data["data"]["user_id"];
                LoginTime = data["data"]["login_time"];
                Exchanges = (string[])data["data"]["exchanges"].ToArray(typeof(string));
                OrderTypes = (string[])data["data"]["order_types"].ToArray(typeof(string));
                Email = data["data"]["email"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string APIKey { get; }
        public string[] Products { get; }
        public string UserName { get; }
        public string Broker { get; }
        public string AccessToken { get; }
        public string PublicToken { get; }
        public string RefreshToken { get; }
        public string UserType { get; }
        public string UserId { get; }
        public string LoginTime { get; }
        public string[] Exchanges { get; }
        public string[] OrderTypes { get; }
        public string Email { get; }
    }

    /// <summary>
    /// Quote structure
    /// </summary>
    public struct Quote
    {
        public Quote(Dictionary<string, dynamic> data)
        {
            try
            {
                Volume = data["volume"];
                LastQuantity = data["last_quantity"];
                LastTime = data["last_time"];
                Change = data["change"];
                OpenInterest = data["open_interest"];
                SellQuantity = data["sell_quantity"];
                ChangePercent = data["change_percent"];
                LastPrice = data["last_price"];
                BuyQuantity = data["buy_quantity"];

                Open = data["ohlc"]["open"];
                Close = data["ohlc"]["close"];
                Low = data["ohlc"]["low"];
                High = data["ohlc"]["high"];

                Bids = new List<DepthItem>();
                Offers = new List<DepthItem>();

                foreach (Dictionary<string, dynamic> bid in data["depth"]["buy"])
                    Bids.Add(new DepthItem(bid));

                foreach (Dictionary<string, dynamic> offer in data["depth"]["sell"])
                    Offers.Add(new DepthItem(offer));
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public int Volume { get; }
        public int LastQuantity { get; }
        public string LastTime { get; }
        public decimal Change { get; }
        public decimal OpenInterest { get; }
        public int SellQuantity { get; }
        public decimal ChangePercent { get; }
        public decimal LastPrice { get; }
        public int BuyQuantity { get; }
        public decimal Open { get; }
        public decimal Close { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public List<DepthItem> Bids { get; }
        public List<DepthItem> Offers { get; }
    }

    /// <summary>
    /// OHLC Quote structure
    /// </summary>
    public struct OHLC
    {
        public OHLC(Dictionary<string, dynamic> data)
        {
            try
            {
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                LastPrice = data["last_price"];

                Open = data["ohlc"]["open"];
                Close = data["ohlc"]["close"];
                Low = data["ohlc"]["low"];
                High = data["ohlc"]["high"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }
        public UInt32 InstrumentToken { get; set; }
        public decimal LastPrice { get; }
        public decimal Open { get; }
        public decimal Close { get; }
        public decimal High { get; }
        public decimal Low { get; }
    }

    /// <summary>
    /// LTP Quote structure
    /// </summary>
    public struct LTP
    {
        public LTP(Dictionary<string, dynamic> data)
        {
            try
            {
                InstrumentToken = Convert.ToUInt32(data["instrument_token"]);
                LastPrice = data["last_price"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }
        public UInt32 InstrumentToken { get; set; }
        public decimal LastPrice { get; }
    }

    /// <summary>
    /// Mutual funds holdings structure
    /// </summary>
    public struct MFHolding
    {
        public MFHolding(Dictionary<string, dynamic> data)
        {
            try
            {
                Quantity = data["quantity"];
                Fund = data["fund"];
                Folio = data["folio"];
                AveragePrice = data["average_price"];
                TradingSymbol = data["tradingsymbol"];
                LastPrice = data["last_price"];
                PNL = data["pnl"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public decimal Quantity { get; }
        public string Fund { get; }
        public string Folio { get; }
        public decimal AveragePrice { get; }
        public string TradingSymbol { get; }
        public decimal LastPrice { get; }
        public decimal PNL { get; }
    }

    /// <summary>
    /// Mutual funds instrument structure
    /// </summary>
    public struct MFInstrument
    {
        public MFInstrument(Dictionary<string, dynamic> data)
        {
            try
            {
                TradingSymbol = data["tradingsymbol"];
                AMC = data["amc"];
                Name = data["name"];

                PurchaseAllowed = Convert.ToInt32(data["purchase_allowed"]);
                RedemtpionAllowed = Convert.ToInt32(data["redemption_allowed"]);

                MinimumPurchaseAmount = Convert.ToDecimal(data["minimum_purchase_amount"]);
                PurchaseAmountMultiplier = Convert.ToDecimal(data["purchase_amount_multiplier"]);
                MinimumAdditionalPurchaseAmount = Convert.ToDecimal(data["minimum_additional_purchase_amount"]);
                MinimumRedemptionQuantity = Convert.ToDecimal(data["minimum_redemption_quantity"]);
                RedemptionQuantityMultiplier = Convert.ToDecimal(data["redemption_quantity_multiplier"]);
                LastPrice = Convert.ToDecimal(data["last_price"]);

                DividendType = data["dividend_type"];
                SchemeType = data["scheme_type"];
                Plan = data["plan"];
                SettlementType = data["settlement_type"];
                LastPriceDate = data["last_price_date"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string TradingSymbol { get; }
        public string AMC { get; }
        public string Name { get; }

        public int PurchaseAllowed { get; }
        public int RedemtpionAllowed { get; }

        public decimal MinimumPurchaseAmount { get; }
        public decimal PurchaseAmountMultiplier { get; }
        public decimal MinimumAdditionalPurchaseAmount { get; }
        public decimal MinimumRedemptionQuantity { get; }
        public decimal RedemptionQuantityMultiplier { get; }
        public decimal LastPrice { get; }

        public string DividendType { get; }
        public string SchemeType { get; }
        public string Plan { get; }
        public string SettlementType { get; }
        public string LastPriceDate { get; }
    }

    /// <summary>
    /// Mutual funds order structure
    /// </summary>
    public struct MFOrder
    {
        public MFOrder(Dictionary<string, dynamic> data)
        {
            try
            {
                StatusMessage = data["status_message"];
                PurchaseType = data["purchase_type"];
                PlacedBy = data["placed_by"];
                Amount = data["amount"];
                Quantity = data["quantity"];
                SettlementId = data["settlement_id"];
                OrderTimestamp = data["order_timestamp"];
                AveragePrice = data["average_price"];
                TransactionType = data["transaction_type"];
                ExchangeOrderId = data["exchange_order_id"];
                ExchangeTimestamp = data["exchange_timestamp"];
                Fund = data["fund"];
                Variety = data["variety"];
                Folio = data["folio"];
                Tradingsymbol = data["tradingsymbol"];
                Tag = data["tag"];
                OrderId = data["order_id"];
                Status = data["status"];
                LastPrice = data["last_price"];
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string StatusMessage { get; }
        public string PurchaseType { get; }
        public string PlacedBy { get; }
        public decimal Amount { get; }
        public decimal Quantity { get; }
        public string SettlementId { get; }
        public string OrderTimestamp { get; }
        public decimal AveragePrice { get; }
        public string TransactionType { get; }
        public string ExchangeOrderId { get; }
        public string ExchangeTimestamp { get; }
        public string Fund { get; }
        public string Variety { get; }
        public string Folio { get; }
        public string Tradingsymbol { get; }
        public string Tag { get; }
        public string OrderId { get; }
        public string Status { get; }
        public decimal LastPrice { get; }
    }

    /// <summary>
    /// Mutual funds SIP structure
    /// </summary>
    public struct MFSIP
    {
        public MFSIP(Dictionary<string, dynamic> data)
        {
            try
            {
                DividendType = data["dividend_type"];
                PendingInstalments = data["pending_instalments"];
                Created = data["created"];
                LastInstalment = data["last_instalment"];
                TransactionType = data["transaction_type"];
                Frequency = data["frequency"];
                InstalmentDate = data["instalment_date"];
                Fund = data["fund"];
                SIPId = data["sip_id"];
                Tradingsymbol = data["tradingsymbol"];
                Tag = data["tag"];
                InstalmentAmount = data["instalment_amount"];
                Instalments = data["instalments"];
                Status = data["status"];
                OrderId = data.ContainsKey(("order_id")) ? data["order_id"] : "";
            }
            catch (Exception)
            {
                throw new DataException("Unable to parse data. " + Utils.JsonSerialize(data));
            }

        }

        public string DividendType { get; }
        public int PendingInstalments { get; }
        public string Created { get; }
        public string LastInstalment { get; }
        public string TransactionType { get; }
        public string Frequency { get; }
        public int InstalmentDate { get; }
        public string Fund { get; }
        public string SIPId { get; }
        public string Tradingsymbol { get; }
        public string Tag { get; }
        public int InstalmentAmount { get; }
        public int Instalments { get; }
        public string Status { get; }
        public string OrderId { get; }
    }

}
