import { PageHead } from './pageHead';
import { ContentEntry } from './content-entry';
import { DetailEntry } from './detail-entry';

export interface PageAndContents{
    pageHeader: PageHead;
    content: string;
    contents: ContentEntry[];
    details: DetailEntry[];
}