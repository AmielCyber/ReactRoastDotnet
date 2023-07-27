const baseURL = import.meta.env.VITE_API_URL as string;

const responseBody = (response: Response) => response.body;

const requests = {
    get: (url: string) => fetch(baseURL + url).then(response => response.json()),
    post: (url: string, body: BodyInit) => fetch(url, {
        method: "POST",
        body: body,
    }).then(responseBody),
    put: (url: string, body: BodyInit) => fetch(url, {
        method: "PUT",
        body: body,
    }).then(responseBody),
    delete: (url: string) => fetch(url).then(responseBody),
};

const Menu = {
    products: () => requests.get("products"),
    productDetails: (id: number) => requests.get(`products/${id}`)
}

const TestErrors = {
    get400Error: () => requests.get("errors/bad-request").then(response => response.json()).catch(() => console.log("YOO")),
    getValidationError: () => requests.get("errors/validation-error"),
    get401Error: () => requests.get("errors/unauthorized"),
    get404Error: () => requests.get("errors/not-found"),
    get500Error: () => requests.get("errors/server-error"),
}

const agent = {
    Menu,
    TestErrors
}

export default agent;
