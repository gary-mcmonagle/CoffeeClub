import {Configuration, BeanApi as Bapi, MenuApi as mapi, OrderApi as oapi, CreateOrderDto, UserApi as uapi} from './generated';

const BeanApi = (basePath: string, accessToken: string) => {
    const api = new Bapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getBean: async () => api.getBean()
    }
};

const MenuApi = (basePath: string, accessToken: string) => {
    const api = new mapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getMenu: async () => api.getMenu()
    }
};

const OrderApi = (basePath: string, accessToken: string) => {
    const api = new oapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        createOrder: async (createOrderDto: CreateOrderDto) => api.createOrder({body: createOrderDto}),
        getAssignable: async () => api.getAssignable(),
        getAll: async () => api.getOrder(),
        assign: async (orderId: string) => api.assignOrder({orderId}),
    }
};

const UserApi = (basePath: string, accessToken: string) => {
    const api = new uapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getUser: async () => api.userGet()
    }
}


export default (basePath: string, accessToken: string) => ({
    beanApi: BeanApi(basePath, accessToken),
    menuApi: MenuApi(basePath, accessToken),
    orderApi: OrderApi(basePath, accessToken),
    userApi: UserApi(basePath, accessToken)
})