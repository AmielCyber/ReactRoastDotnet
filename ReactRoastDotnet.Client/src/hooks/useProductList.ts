import useSWR from "swr";
import {useRef} from "react";
// Our imports.
import type ProductList from "../models/ProductList";

const BASE_URL = import.meta.env.VITE_API_URL as string;
const PRODUCTS_URL = BASE_URL + "products?";

const DEFAULT_PAGE_SIZE = 6;

// Let SWR handle all errors.
const productListFetcher = async (pageParams: string): Promise<ProductList> => {
    // Call our endpoint.
    const response: Response = await fetch(PRODUCTS_URL + pageParams, {
        method: "GET",
    });

    if (!response.ok) {
        const responseData = await response.json() as Error;
        // Make SWR catch the failed response.
        throw new Error(responseData?.message || response.statusText);
    }

    // Return result.
    return await response.json() as Promise<ProductList>;
};

// Set revalidation options, fetches data again if true during the following conditions:
const revalidateOptions = {
    revalidateOnFocus: false,
    revalidateIfStale: false,
    revalidateOnReconnect: false,
};

function useProductList(pageParams: string) {
    const currentPageRef = useRef(1); // Maintain current page while fetching data.
    const totalPagesRef = useRef(1); // Maintain total pages while fetching data.
    const pageSizeRef = useRef(DEFAULT_PAGE_SIZE); // Maintain total pages while fetching data.
    const {data, error, isLoading} = useSWR<ProductList, Error>(
        pageParams,
        productListFetcher,
        revalidateOptions
    );

    // Adjust current page or total pages if they had changed.
    if (data?.pagination) {
        if (totalPagesRef.current !== data.pagination.totalPages) {
            totalPagesRef.current = data.pagination.totalPages;
        }
        if (currentPageRef.current !== data.pagination.currentPage) {
            currentPageRef.current = data.pagination.currentPage;
        }
        if (pageSizeRef.current !== data.pagination.pageSize) {
            pageSizeRef.current = data.pagination.pageSize;
        }
    }

    return {
        products: data?.items,
        error: error,
        isLoading: isLoading,
        currentPage: currentPageRef.current,
        totalPages: totalPagesRef.current,
        pageSize: pageSizeRef.current,
    };
}

export default useProductList;
