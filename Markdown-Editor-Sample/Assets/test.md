# Markdown Syntax

- [Overview](#overview)
    - [Philosophy](#philosophy)
    - [Inline HTML](#inline-html)
- [Block Elements](#block-elements)
    - [Paragraphs and Line Breaks](#paragraphs-and-line-breaks)
    - [Headers](#headers)
    - [Blockquotes](#blockquotes)
    - [Lists](#lists)
    - [Check Lists](#check-list)
    - [Code Blocks](#code-blocks)
    - [Table](#table)
    - [Foot Note](#foot-note)
    - [Custom Container](#custom-container)
    - [KaTeX / LaTeX](#katex-latex)
    - [Plantuml](#plantuml)
    - [Horizontal Rules](#horizontal-rules)
- [Span Elements](#span-elements)
    - [Links](#links)
    - [Emphasis](#emphasis)
    - [Markable](#markable)
    - [Abbreviation](#abbreviation)
    - [Emoji](#emoji)
    - [Image](#image)

## Overview

### Philosophy

Markdown is intended to be as easy-to-read and easy-to-write as is feasible.

Readability, however, is emphasized above all else. A Markdown-formatted
document should be publishable as-is, as plain text, without looking
like it's been marked up with tags or formatting instructions. While
Markdown's syntax has been influenced by several existing text-to-HTML
filters -- including [Setext](http://docutils.sourceforge.net/mirror/setext.html), [atx](http://www.aaronsw.com/2002/atx/), [Textile](http://textism.com/tools/textile/), [reStructuredText](http://docutils.sourceforge.net/rst.html),
[Grutatext](http://www.triptico.com/software/grutatxt.html), and [EtText](http://ettext.taint.org/doc/) -- the single biggest source of
inspiration for Markdown's syntax is the format of plain text email.

### Inline HTML

<p>
<em>This</em> <del>is the</del> <code>HTML</code> <b>snippet</b>
</p>

## Block Elements

### Paragraphs and Line Breaks

Markdown segmentation can be achieved by interlacing, or adding two spaces at the end of the paragraph.

Line break after interlacing  
Line break after adding space

### Headers

```markdown
# This is an <h1> tag
## This is an <h2> tag
###### This is an <h6> tag
```

### Blockquotes

```markdown
> This is single blockquote
>> This is a nested blockquote
```

> This is single blockquote
>> This is a nested blockquote

### Lists

* Item 1
* Item 2
  * Item 2a
  * Item 2b

1. Item 1
1. Item 2
1. Item 3
   1. Item 3a
   1. Item 3b

### Check List

- [ ] Item1
- [x] Item2

### Code Blocks

```javascript
function fancyAlert(arg) {
  if(arg) {
    $.facebox({div:'#foo'})
  }
}
```

### Table

```markdown
Stage | Direct Products | ATP Yields
----: | --------------: | ---------:
Glycolysis | 2 ATP ||
^^ | 2 NADH | 3--5 ATP |
Pyruvaye oxidation | 2 NADH | 5 ATP |
Citric acid cycle | 2 ATP ||
^^ | 6 NADH | 15 ATP |
^^ | 2 FADH2 | 3 ATP |
**30--32** ATP |||
```

Stage | Direct Products | ATP Yields
----: | --------------: | ---------:
Glycolysis | 2 ATP ||
^^ | 2 NADH | 3--5 ATP |
Pyruvaye oxidation | 2 NADH | 5 ATP |
Citric acid cycle | 2 ATP ||
^^ | 6 NADH | 15 ATP |
^^ | 2 FADH2 | 3 ATP |
**30--32** ATP |||

### Foot Note

```markdown
Here is a footnote reference,[^1] and another.[^longnote]

[^1]: Here is the footnote.

[^longnote]: Here's one with multiple blocks.

    Subsequent paragraphs are indented to show that they
belong to the previous footnote.
```

Here is a footnote reference,[^1] and another.[^longnote]

[^1]: Here is the footnote.

[^longnote]: Here's one with multiple blocks.

Subsequent paragraphs are indented to show that they
belong to the previous footnote.

### Custom Container

```markdown
:::tip
Some tips.
:::
```

:::tip
Some tips.
:::

:::warning
Some warning.
:::

:::danger
Some danger.
:::

### KaTeX / LaTeX

```markdown
$E=mc^2$

$$\begin{array}{c}

\nabla \times \vec{\mathbf{B}} -\, \frac1c\, \frac{\partial\vec{\mathbf{E}}}{\partial t} &
= \frac{4\pi}{c}\vec{\mathbf{j}}    \nabla \cdot \vec{\mathbf{E}} & = 4 \pi \rho \\

\nabla \times \vec{\mathbf{E}}\, +\, \frac1c\, \frac{\partial\vec{\mathbf{B}}}{\partial t} & = \vec{\mathbf{0}} \\

\nabla \cdot \vec{\mathbf{B}} & = 0

\end{array}$$
```

$E=mc^2$

$$\begin{array}{c}

\nabla \times \vec{\mathbf{B}} -\, \frac1c\, \frac{\partial\vec{\mathbf{E}}}{\partial t} &
= \frac{4\pi}{c}\vec{\mathbf{j}}    \nabla \cdot \vec{\mathbf{E}} & = 4 \pi \rho \\

\nabla \times \vec{\mathbf{E}}\, +\, \frac1c\, \frac{\partial\vec{\mathbf{B}}}{\partial t} & = \vec{\mathbf{0}} \\

\nabla \cdot \vec{\mathbf{B}} & = 0

\end{array}$$

### Plantuml

```markdown
@startuml
(*) --> "Initialization"

if "Some Test" then
  -->[true] "Some Activity"
  --> "Another activity"
  -right-> (*)
else
  ->[false] "Something else"
  -->[Ending process] (*)
endif

@enduml
```

@startuml
(*) --> "Initialization"

if "Some Test" then
  -->[true] "Some Activity"
  --> "Another activity"
  -right-> (*)
else
  ->[false] "Something else"
  -->[Ending process] (*)
endif

@enduml

### Horizontal Rules

```markdown
The line  
---
```

---

## Span Elements

### Links

```markdown
[Richasy's Github](https://github.com/Richasy)
```

[Richasy's Github](https://github.com/Richasy)

### Emphasis

```markdown
### Strong
I just love **bold text**.

### Italic
I just love *bold text*.

### Bold and Italic
This text is ***really important***.

### Delete
This is ~~delete~~ text

### Insert
This is ++insert++ text

### Highlight
This is ==Highlight== text

### Inline code
We need `markdown` editor
```

I just love **bold text**.

I just love *bold text*.

This text is ***really important***.

This is ~~delete~~ text

This is ++insert++ text

This is ==Highlight== text

We need `markdown` editor

### Markable

```markdown
### Subscript
H~2~O~2~

### Supscript
X^2^ + Y^2^ = Z^2^
```

H~2~O~2~

X^2^ + Y^2^ = Z^2^

### Abbreviation

```markdown
*[HTML]: Hyper Text Markup Language
*[W3C]:  World Wide Web Consortium
The HTML specification
is maintained by the W3C.
```

*[HTML]: Hyper Text Markup Language
*[W3C]:  World Wide Web Consortium
The HTML specification
is maintained by the W3C.

### Emoji

```markdown
:smile:
:)
```

:cupid: Hi, The total emoji list in here: [Github](https://gist.github.com/rxaviers/7360908)

### Image

```markdown
![Image name](http://iph.href.lu/879x200?fg=666666&bg=cccccc "Image Description")
```

![Image name](http://iph.href.lu/879x200?fg=666666&bg=cccccc "Image Description")