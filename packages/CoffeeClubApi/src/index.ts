export const Greeter = (name: string) => `Hello ${name}`; 
import {Configuration, DevTestApi, BeanApi as Bapi, MenuApi as mapi, OrderApi as oapi, CreateOrderDto, UserApi as uapi} from './generated';

export const DevTest = (basePath: string) => {
    const api = new DevTestApi(new Configuration({basePath}));
    return {
        getDt: async () => api.devTestGet()
    }
}

export const BeanApi = (basePath: string, accessToken: string) => {
    const api = new Bapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getBean: async () => api.beanGet()
    }
};

export const MenuApi = (basePath: string, accessToken: string) => {
    const api = new mapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getMenu: async () => api.menuGet()
    }
};

export const OrderApi = (basePath: string, accessToken: string) => {
    const api = new oapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        createOrder: async (createOrderDto: CreateOrderDto) => api.orderPost({createOrderDto}),
        getAssignable: async () => api.orderAssignableGet(),
        assign: async (orderId: string) => api.orderOrderIdAssignPost({orderId}),
    }
};

export const UserApi = (basePath: string, accessToken: string) => {
    const api = new uapi(new Configuration({basePath, headers: {Authorization: `Bearer ${accessToken}`}}));
    return {
        getUser: async () => api.userGet()
    }
}


export default (basePath: string, accessToken: string) => ({
    bean: BeanApi(basePath, accessToken),
    menu: MenuApi(basePath, accessToken),
    order: OrderApi(basePath, accessToken)
})