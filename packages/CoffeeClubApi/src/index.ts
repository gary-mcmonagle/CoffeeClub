import {Configuration, DevTestApi, BeanApi as Bapi, MenuApi as mapi, OrderApi as oapi, CreateOrderDto, UserApi as uapi} from './generated';

const DevTest = (basePath: string) => {
    const api = new DevTestApi(new Configuration({basePath}));
    return {
        getDt: async () => api.devTestGet()
    }
}

const BeanApi = (basePath: string, accessToken: string) => {
    const api = new Bapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getBean: async () => api.beanGet()
    }
};

const MenuApi = (basePath: string, accessToken: string) => {
    const api = new mapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getMenu: async () => api.menuGet()
    }
};

const OrderApi = (basePath: string, accessToken: string) => {
    const api = new oapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        createOrder: async (createOrderDto: CreateOrderDto) => api.orderPost({createOrderDto}),
        getAssignable: async () => api.orderAssignableGet(),
        getAll: async () => api.orderGet(),
        assign: async (orderId: string) => api.orderOrderIdAssignPost({orderId}),
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