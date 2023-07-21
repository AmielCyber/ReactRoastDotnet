import PageStats from "./PageStats.tsx";
import PageButtons from "./PageButtons.tsx";

type Props = {
    pageSize: number;
    currentPage: number;
    totalPages: number;
    onPageChange: (pageNumber: number) => void;
}

function Pagination(props: Props) {
    const hasNext = props.currentPage !== props.totalPages;
    const hasPrev = props.currentPage > 1;

    const onNext = () => {
        if (hasNext) {
            props.onPageChange(props.currentPage + 1);
        }
    };
    const onPrev = () => {
        if (hasPrev) {
            props.onPageChange(props.currentPage - 1);
        }
    }
    return (
        <div className="max-w-screen-lg flex-col justify-center items-center ml-auto mr-auto">
            <PageStats pageSize={props.pageSize} currentPage={props.currentPage} totalPages={props.totalPages}/>
            <PageButtons onPrevious={onPrev} onNext={onNext} hasPrevious={hasPrev} hasNext={hasNext}/>
        </div>
    );
}

export default Pagination;
