// My imports.
import DisplayErrorAlert from "../error/DisplayErrorAlert.tsx";
import MenuItem from "./MenuItem.tsx";
import useProductList from "../hooks/useProductList.ts";
import Pagination from "../pagination/Pagination.tsx";

type Props = {
    pageSearchParams: string;
    onPageChange: (pageNumber: number) => void;
}

function MenuList(props: Props) {
    const {
        products,
        error,
        isLoading,
        pageSize,
        totalPages,
        currentPage
    } = useProductList(props.pageSearchParams);

    if (isLoading) {
        return (
            <div className="flex">
                <progress className="progress w-56 mx-auto progress-secondary"/>
            </div>
        );
    }
    if (error || products === undefined) {
        return <DisplayErrorAlert/>
    }
    return (
        <>
            <ul className="flex flex-col w-full border-opacity-50 px-2">
                {products.map(item =>
                    <li key={item.id}>
                        <MenuItem productItem={item}/>
                        <div className="divider"/>
                    </li>
                )}
            </ul>
            <Pagination
                pageSize={pageSize}
                totalPages={totalPages}
                currentPage={currentPage}
                onPageChange={props.onPageChange}
            />
        </>
    );
}

export default MenuList;
