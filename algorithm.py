stock_sids = [sid(50368), sid(24692), sid(20088), sid(3212), sid(700), sid(39778), sid(8347), sid(4799), sid(8229), sid(6683)]

stocks_weight = [100, 100, 650, 200, 150, 200, 100, 150, 200, 150]

def initialize(context):
    context.aapl = stock_sids
    schedule_function(trading_parameter,
                      date_rules.every_day(),
                      time_rules.market_open(hours=1))

def count(values):
    x = 0
    for i in values:
        x = x + 1
    return x

def moving_average_calc(values):
    for x in values:
        return (sum(values)/count(values))

def relative_strength(values):
    return 0

def exponential_moving_average(data):
    return 0

def trading_parameter(context, data):
    starting_cash = 2000
    #cash = context.portfolio.cash
    i = 0
    for stock in stock_sids:
        cash_per_stock = stocks_weight[i]

        stock_price_data = data.history(stock, "price", 20, "1d")
        today_price = stock_price_data[0]

        last_twenty = []
        #create moving average values
        for x in range(20):
            last_twenty.append(stock_price_data[x])

        moving_average = moving_average_calc(last_twenty)

        if(today_price > moving_average):
            order_target_percent(stock, (cash_per_stock/starting_cash))
        elif(today_price < moving_average):
            order_target_percent(stock, 0)
        i = i + 1
