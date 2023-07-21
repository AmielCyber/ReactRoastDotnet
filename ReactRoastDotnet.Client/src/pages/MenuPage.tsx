import {useEffect} from "react";
import {useSearchParams} from "react-router-dom";
// My imports.
import {getURLPageParams, pageParamType} from "../utils/paginationHelper";
import MenuHeader from "../components/menu/MenuHeader";
import MenuList from "../components/menu/MenuList";

const scrollOptions: ScrollToOptions = {
    top: 0,
    left: 0,
    behavior: "smooth"
}

// TODO: Add handlers for other pageParams change
function MenuPage() {
    const [searchParams, setSearchParams] = useSearchParams();

    useEffect(() => {
        // Scroll to top of page on page change.
        window.scrollTo(scrollOptions)
    }, [searchParams])

    const newPageParams = getURLPageParams(searchParams);

    const handlePageChange = (pageNumber: number) => {
        if (pageNumber > 0) {
            newPageParams.set(pageParamType.pageNum, pageNumber.toString());
            setSearchParams(newPageParams);
        }
    }

    return (
        <main className="max-w-screen-lg mx-auto pb-8 mb-20 md:mb-0">
            <div className="flex z-0 flex-col justify-center items-center">
                <MenuHeader/>
            </div>
            <MenuList pageSearchParams={newPageParams.toString()} onPageChange={handlePageChange}/>
        </main>
    );
}

export default MenuPage;
